using DevGpt.Models.Commands;
using HtmlAgilityPack;

namespace DevGpt.Console.Commands;

public class BrowseWebsiteCommand : ICommand
{
    public string Execute(params string[] args)
    {
        if (args.Length != 1)
        {
            return $"{Name} requires 1 argument: url";
        }

        try
        {
            var url = args[0];
            var web = new HtmlWeb();
            var doc = web.Load(url);

            return doc.DocumentNode.InnerText;

        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }
    public string Name => "browse_website";
    public string Description => "browses a website and returns the text on the page";
    public string[] Arguments => new[]{"url"};

}