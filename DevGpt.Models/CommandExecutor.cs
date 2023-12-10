using System.Drawing;
using System.Text.Json;
using System.Text.Json.Nodes;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using DevGpt.Models.Utils;

namespace MyApp;

public interface ICommandExecutor
{
    Task<IEnumerable<DevGptChatMessage>> Execute(string commandName, string[] args);
}


public class CommandExecutor : ICommandExecutor
{
    private readonly IList<ICommandBase> _commands;

    public CommandExecutor(IList<ICommandBase> commands)
    {
        _commands = commands;
    }

    public async Task<IEnumerable<DevGptChatMessage>> ExecuteTool(DevGptToolCall toolCall)
    {
        var devGptChatMessages = await Execute(toolCall.ToolName, toolCall.Arguments.ToArray());

        foreach (var message in devGptChatMessages)
        {
            if (message is DevGptToolCallResultMessage toolCallResultMessage)
            {
                toolCallResultMessage.ToolCallId = toolCall.ToolCallId;
            }
        }

        return devGptChatMessages;
    }

    public async Task<IEnumerable<DevGptChatMessage>> Execute(string commandName, string[] args)

    {
        DevConsole.WriteLine($"Do you want to execute {commandName} ? (y/n)");
        var response = System.Console.ReadLine();
        if (response.ToLower() == "y")
        {
            return await DoExecute(commandName,
                args);
        }

        return new[] { new DevGptChatMessage(DevGptChatRole.User, "User refused to execute command. Please try something else") };

    }

    private async Task<IEnumerable<DevGptChatMessage>> DoExecute(string commandName, string[] args)
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
            return new[]
            {
                new DevGptToolCallResultMessage(commandName,
                    $"command {commandName} not found. Please make sure you use on the following commands.\r\n{commandsText}")
            };
        }
        
        if (command is IAsyncMessageCommand messageCommand)
        {
            return await messageCommand.ExecuteAsync(args);
        }

        if (command is IAsyncCommand asyncCommand)
        {
            return new[]
            {

                new DevGptToolCallResultMessage(commandName, await asyncCommand.ExecuteAsync(args))
            };
        }

        var result = (command as ICommand)?.Execute(args) ??
                        throw new InvalidOperationException();
        return new[]
        {
            new DevGptToolCallResultMessage(commandName, result)
        };
    }
}