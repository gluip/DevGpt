using DevGpt.Console;
using DevGpt.Console.Commands;
using DevGpt.Console.Prompts;
using DevGpt.Models.Commands;

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
            var commands = new PromptGenerator_Accountant().GetCommandsText(_commands);
            return $"command {commandName} not found. Please make sure you use on the following commands.\r\n{commands}";
        }
        return command.Execute(args);
        
    }
}