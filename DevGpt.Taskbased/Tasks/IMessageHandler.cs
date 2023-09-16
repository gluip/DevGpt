using Azure.AI.OpenAI;

namespace DevGpt.Console.Tasks;

internal interface IMessageHandler
{
    void HandleMessage(ChatRole chatRole, string message);
}