using Azure.AI.OpenAI;

namespace DevGpt.Console.Models;

public class DevGptChatMessage:ChatMessage
{
    public DevGptChatMessage(ChatRole role, string content) : base(role, content)
    {
    }

    public int Index { get; set; }
}