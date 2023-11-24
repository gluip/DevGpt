using System.Drawing;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using DevGpt.Models.Utils;

namespace MyApp;

public interface ICommandExecutor
{
    Task<DevGptChatMessage> Execute(string commandName, string[] args);
}


public class CommandExecutor : ICommandExecutor
{
    private readonly IList<ICommandBase> _commands;

    public CommandExecutor(IList<ICommandBase> commands)
    {
        _commands = commands;
    }

    public async Task<DevGptChatMessage> Execute(string commandName, string[] args)

    {
        DevConsole.WriteLine($"Do you want to execute {commandName} ? (y/n)");
        var response = System.Console.ReadLine();
        if (response.ToLower() == "y")
        {
            return await DoExecute(commandName,
                args);
        }

        return new DevGptChatMessage(DevGptChatRole.User, "User refused to execute command. Please try something else");

    }

    private async Task<DevGptChatMessage> DoExecute(string commandName, string[] args)
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
            return new DevGptChatMessage(DevGptChatRole.User,
                $"command {commandName} not found. Please make sure you use on the following commands.\r\n{commandsText}");
        }

        if (command is IAsyncMessageCommand messageCommand)
        {
            return await messageCommand.ExecuteAsync(args);
        }

        if (command is IAsyncCommand asyncCommand)
        {
            var stringResult = await asyncCommand.ExecuteAsync(args);
            return new DevGptChatMessage(DevGptChatRole.User, stringResult);
        }

        var doExecute = (command as ICommand)?.Execute(args) ??
                        throw new InvalidOperationException();
        return new DevGptChatMessage(DevGptChatRole.User, doExecute);
    }
}