namespace DevGpt.Models;

public static class EmbeddedResourceReader
{
    public static string GetEmbeddedResourceText(string name)
    {
        var embeddedResource = System.Reflection.Assembly.GetCallingAssembly()
            .GetManifestResourceStream(name);
        using var reader = new System.IO.StreamReader(embeddedResource);
        return reader.ReadToEnd();
    }
}