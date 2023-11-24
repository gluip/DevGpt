using DevGpt.Models.OpenAI;

namespace DevGpt.Models.Commands;

public interface IAsyncMessageCommand : ICommandBase
{
    Task<DevGptChatMessage> ExecuteAsync(string[] args);
}