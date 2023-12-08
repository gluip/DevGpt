using DevGpt.Models.OpenAI;
using OpenAI.Chat;

namespace DevGpt.OpenAIDotnet;

public class DotnetChatMessageMapper
{
    public static IList<Message> Map(DevGptToolCallResultMessage? toolCallResultMessage)
    {
        
        return new[]
        {
            toolCallResultMessage.ToolCallMessage, 
            
            new Message( toolCallResultMessage.ToolCallMessage.ToolCalls.First(),toolCallResultMessage.Result)
        };

    }
    public static Message Map(DevGptChatMessage message)
    {
        var role = Map(message.Role);
        var contents = message.Content.Select(Map);
        return new Message(role, contents);
    }

    private static Content Map(DevGptContent content)
    {
        return new Content(Map(content.ContentType),content.Content);
            
    }

    private static ContentType Map(DevGptContentType contentContentType)
    {
        return contentContentType == DevGptContentType.ImageUrl ? 
            ContentType.ImageUrl : ContentType.Text;
    }

    private static Role Map(DevGptChatRole messageRole)
    {
        return messageRole == DevGptChatRole.Assistant ? Role.Assistant :
            messageRole == DevGptChatRole.User ? Role.User : 
            messageRole == DevGptChatRole.Tool? Role.Tool :
            Role.System;
    }
}