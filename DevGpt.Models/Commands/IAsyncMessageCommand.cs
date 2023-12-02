using DevGpt.Models.OpenAI;

namespace DevGpt.Models.Commands;

public interface IAsyncMessageCommand : ICommandBase
{
    Task<IList<DevGptChatMessage>> ExecuteAsync(string[] args);
}