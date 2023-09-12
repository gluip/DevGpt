using DevGpt.Console;
using DevGpt.Console.Commands;
using DevGpt.Console.Prompts;
using DevGpt.Models.Commands;

namespace MyApp;

internal class CommandExecutor
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
            var commands = new PromptGenerator_Accountant().GetCommandsText(_commands);
            return new ComplexResult
            {
                Result =
                    $"command {commandName} not found. Please make sure you use on the following commands.\r\n{commands}"
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