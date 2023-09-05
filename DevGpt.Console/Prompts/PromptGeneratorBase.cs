using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts;

internal abstract class PromptGeneratorBase : IPromptGenerator
{
    public string GetFullPrompt(IList<ICommandBase> commands)
    {
        var commandsText = GetCommandsText(commands);
        return GetUserPrompt(commandsText);
    }

    public abstract string GetUserPrompt(string commandsText);


    public string GetCommandsText(IList<ICommandBase> commands)
    {
        var commandsText = string.Join("\n", commands.Select(c => c.GetHelp()));
        commandsText += "\n\n";
        return commandsText;
    }

    public const string SystemPrompt = "You are an AI assistant that helps people in coding tasks.";
}