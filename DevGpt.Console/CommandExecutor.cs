using DevGpt.Console.Commands;

namespace MyApp;

internal class CommandExecutor
{
    private readonly IList<ICommand> _commands;

    public CommandExecutor(IList<ICommand> commands)
    {
        _commands = commands;
    }
    public string Execute(string commandName, string[] args)
    {
        var command = _commands.FirstOrDefault(c => c.Name == commandName);
        if (command == null)
        {
            return $"command {commandName} not found";
        }
        return command.Execute(args);
        
    }
}