using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts;

internal abstract class PromptGeneratorBase : IPromptGenerator
{
    public string GetFullPrompt(IList<ICommandBase> commands)
    {
        var commandsText = commands.GetCommandsText();

        return GetUserPrompt(commandsText);
    }

    public abstract string GetUserPrompt(string commandsText);

    protected string GetGenericPromt()
    {
        return EmbeddedResourceReader.GetEmbeddedResourceText("DevGpt.Console.Prompts.Generic.MainPrompt.txt");
    }
}