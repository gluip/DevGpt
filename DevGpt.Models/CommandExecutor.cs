using DevGpt.Models.Commands;

namespace MyApp;

public interface ICommandExecutor
{
    Task<string> Execute(string commandName, string[] args);
}


public class CommandExecutor:ICommandExecutor
{
    private readonly IList<ICommandBase> _commands;

    public CommandExecutor(IList<ICommandBase> commands)
    {
        _commands = commands;
    }
    public async Task<string> Execute(string commandName, string[] args)
    {
        // remove double encoding from args
        for (int i = 0; i < args.Length; i++)
        {
            args[i] = args[i].Replace("\\n", "\n").Replace("\\r", "\r");
        }


        var command = _commands.FirstOrDefault(c => c.Name == commandName);
        if (command == null)
        {
            var commandsText = string.Join("\n", _commands.Select(c => c.GetHelp()));
            commandsText += "\n\n";
            return $"command {commandName} not found. Please make sure you use on the following commands.\r\n{commandsText}";
        }

        if (command is IAsyncCommand asyncCommand)
        {
            return await asyncCommand.ExecuteAsync(args);
        }
        return (command as ICommand)?.Execute(args) ??
               throw new InvalidOperationException();

    }
}