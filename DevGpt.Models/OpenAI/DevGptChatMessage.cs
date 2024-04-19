using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;


namespace DevGpt.Models.OpenAI;



public enum DevGptChatRole
{
    User, System, Assistant, ContextMessage,Tool
}
public enum DevGptContentType
{
    ImageUrl, Text
}

public class DevGptContent
{
    public DevGptContent(DevGptContentType type, string content)
    {
        ContentType = type;
        Content = content;
    }

    public DevGptContentType ContentType { get; set; }
    public string Content { get; set; }
}

public class DevGptContextMessage : DevGptChatMessage
{
    public string ContextKey { get; }

    public DevGptContextMessage(string contextKey,string content) : base(DevGptChatRole.ContextMessage, content)
    {
        ContextKey = contextKey;
        Role = DevGptChatRole.ContextMessage;
    }

    public DevGptContextMessage(string contextKey, IList<DevGptContent> content) : base(DevGptChatRole.ContextMessage, content)
    {
        ContextKey = contextKey;
        Role = DevGptChatRole.ContextMessage;
    }
}

public class DevGptToolCallResultMessage : DevGptChatMessage
{
    public DevGptToolCallResultMessage(string toolName, string result) : base(
        DevGptChatRole.Tool, result)
    {
        ToolName = toolName;
    }
    public DevGptToolCallResultMessage(string toolName, IList<DevGptContent> content) : base(
        DevGptChatRole.Tool, content)
    {
        ToolName = toolName;
    }

    public string ToolName { get; }
    public string ToolCallId { get; set; }
}



public class DevGptChatMessage
{
    public DevGptChatMessage(DevGptChatRole role, string content)
    {
        Role = role;
        Content = new[]
        {
            new DevGptContent(DevGptContentType.Text, content)
        };
    }

    public DevGptChatMessage(DevGptChatRole role, IList<DevGptContent> content)
    {
        Role = role;
        Content = content;
    }

    public DevGptChatMessage(DevGptChatRole role, string messageContent,
        IList<DevGptToolCall> devGptToolCalls) : this(role, messageContent)
    {
        ToolCalls = devGptToolCalls;
    }

    public IList<DevGptToolCall> ToolCalls { get; set; } = new List<DevGptToolCall>();

    public DevGptChatRole Role { get; set; }
    public IList<DevGptContent> Content { get; }

    public string ToString()
    {
        if (Content.Count == 1 && Content[0].ContentType == DevGptContentType.Text)
        {
            return $"{Role}:{Content[0].Content}";
        }
        else
        {
            return JsonSerializer.Serialize(this);
        }
        
    }

    public bool IsContext { get; set; }
}

public class DevGptToolCall
{
    //TODO: remove this OpenAI Dotnet reference
    public DevGptToolCall(string toolName, List<string?> arguments, string toolCallId)
    {
        ToolName = toolName;
        Arguments = arguments;
        ToolCallId = toolCallId;
    }

    public string ToolName { get; }
    public IList<string> Arguments { get; }
    public string ToolCallId { get; }

    public override string ToString()
    {
        return $"{ToolName}({string.Join(",", Arguments)})";
    }
}

public interface IDevGptOpenAIClient
{
    Task<DevGptChatMessage> CompletePrompt(IList<DevGptChatMessage> allMessages, IList<ICommandBase> commands = null);
}