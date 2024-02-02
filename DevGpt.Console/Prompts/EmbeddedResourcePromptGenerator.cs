namespace DevGpt.Console.Prompts;

internal class EmbeddedResourcePromptGenerator : PromptGeneratorBase
{
    private readonly string _embeddedResourceName;

    public EmbeddedResourcePromptGenerator(string embeddedResourceName)
    {
        _embeddedResourceName = $"DevGpt.Console.Prompts.{embeddedResourceName}";
    }

    public override string GetUserPrompt(string commandsText)
    {
        return EmbeddedResourceReader.GetEmbeddedResourceText(_embeddedResourceName) + GetGenericPromt();
    }

   
}