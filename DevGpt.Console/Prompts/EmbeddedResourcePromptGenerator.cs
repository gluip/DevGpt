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
        return GetEmbeddedResourceText() + GetGenericPromt();
    }

    private string GetEmbeddedResourceText()
    {
        var embeddedResource = System.Reflection.Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(_embeddedResourceName);
        using var reader = new System.IO.StreamReader(embeddedResource);
        return reader.ReadToEnd();
    }
}