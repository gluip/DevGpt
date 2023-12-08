using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using OpenAI.Chat;

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
        Result = result;
    }

    public string ToolName { get; }
    public string Result { get; set; }
    public Message ToolCallMessage { get; set; }
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
    public DevGptToolCall(string toolName, IList<string> arguments, Message toolcallMessage)
    {
        ToolName = toolName;
        Arguments = arguments;
        ToolcallMessage = toolcallMessage;
    }

    public string ToolName { get; }
    public IList<string> Arguments { get; }
    public Message ToolcallMessage { get; }

    public override string ToString()
    {
        return $"{ToolName}({string.Join(",", Arguments)})";
    }
}
public class DevGptChatResponse
{
    public DevGptChatResponse(string message, IList<DevGptToolCall> toolCalls)
    {
        Message = message;
        ToolCalls = toolCalls;
    }

    public string Message { get; }
    public IList<DevGptToolCall> ToolCalls { get; }
}


public interface IDevGptOpenAIClient
{
    Task<DevGptChatResponse> CompletePrompt(IList<DevGptChatMessage> allMessages, IList<ICommandBase> commands = null);
}