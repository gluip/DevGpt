using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DevGpt.Models.OpenAI;



public enum DevGptChatRole
{
    User, System, Assistant, ContextMessage
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
public interface IDevGptOpenAIClient
{
    Task<string> CompletePrompt(IList<DevGptChatMessage> allMessages);
}