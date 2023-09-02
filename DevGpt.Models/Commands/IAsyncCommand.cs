namespace DevGpt.Models.Commands;

public interface IAsyncCommand : ICommandBase
{
    Task<string> ExecuteAsync(string[] args);
}