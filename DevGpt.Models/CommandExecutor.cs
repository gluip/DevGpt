using DevGpt.Models.Commands;

namespace MyApp;

public interface ICommandExecutor
{
    Task<ComplexResult> Execute(string commandName, string[] args);
}

public class CommandExecutor : ICommandExecutor
{
    private readonly IList<ICommandBase> _commands;

    public CommandExecutor(IList<ICommandBase> commands)
    {
        _commands = commands;
    }
    public async Task<ComplexResult> Execute(string commandName, string[] args)
    {


        // remove double encoding from args
        for (int i = 0; i < args.Length; i++)
        {
            args[i] = args[i].Replace("\\n", "\n").Replace("\\r","\r");
        }
        
        var command = _commands.FirstOrDefault(c => c.Name == commandName);
        if (command == null)
        {
            var commandsText = string.Join("\n", _commands.Select(c => c.GetHelp()));
            commandsText += "\n\n";

            return new ComplexResult
            {
                Result =
                    $"command {commandName} not found. Please make sure you use one of the following commands.\r\n{commandsText}"
            };
        }

        if (command is IAsyncCommand asyncCommand)
        {
            return new ComplexResult { Result = await asyncCommand.ExecuteAsync(args) };
        }

        if (command is IComplexCommand complexCommand)
        {
            return await complexCommand.ExecuteAsync(args);
        }

        var result = (command as ICommand)?.Execute(args); 
       

        return result != null ? new ComplexResult{Result = result}:throw new InvalidOperationException();
        
    }
}