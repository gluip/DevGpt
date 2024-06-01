using DevGpt.Models.OpenAI;
using OpenAI;
using OpenAI.Chat;

namespace DevGpt.OpenAIDotnet;

public class DotnetChatMessageMapper
{
    
    public static Message Map(DevGptChatMessage message)
    {
        var role = Map(message.Role);
        var contents = message.Content.Select(Map);
        return new Message(role, contents);
    }

    public static Content Map(DevGptContent content)
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
            messageRole == DevGptChatRole.ContextMessage ? Role.User:
            Role.System;
    }
}