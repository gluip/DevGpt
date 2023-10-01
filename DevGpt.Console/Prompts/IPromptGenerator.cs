using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts;

internal interface IPromptGenerator
{
    string GetFullPrompt(IList<ICommandBase> commands);
}