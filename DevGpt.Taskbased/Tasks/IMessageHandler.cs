
using DevGpt.Models.OpenAI;

namespace DevGpt.Console.Tasks;

internal interface IMessageHandler
{
    void HandleMessage(DevGptChatRole chatRole, string message);
}