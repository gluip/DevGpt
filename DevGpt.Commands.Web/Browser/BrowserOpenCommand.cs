using DevGpt.Models.Commands;
using HtmlAgilityPack;

namespace DevGpt.Commands.Web.Browser;

public class BrowserOpenCommand : IAsyncCommand
{
    private readonly IBrowser _browser;

    public BrowserOpenCommand(IBrowser browser)
    {
        _browser = browser;
    }

    public async Task<string> ExecuteAsync(params string[] args)
    {
        if (args.Length != 1)
        {
            return $"{Name} requires 1 argument: url";
        }

        try
        {
            return await _browser.OpenPage(args[0]);

        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }
    public string Name => "browser_open_page";
    public string Description => "opens the browser for the specific page";
    public string[] Arguments => new[] { "url" };

}