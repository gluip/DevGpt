using DevGpt.Models.Commands;
using HtmlAgilityPack;

namespace DevGpt.Commands.Web.Browser;

public class BrowserClickCommand : IAsyncCommand
{
    private readonly IBrowser _browser;

    public BrowserClickCommand(IBrowser browser)
    {
        _browser = browser;
    }

    public async Task< string> ExecuteAsync(params string[] args)
    {
        if (args.Length != 1)
        {
            return $"{Name} requires 1 argument: css selector";
        }

        try
        {
            await _browser.ClickAsync(args[0]);
            return $"Element {args[0]} clicked";
        }
        catch (TimeoutException e)
        {
            return "No element found for selector";
        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }
    public string Name => "browser_click";
    public string Description => "clicks on a element on a page";
    public string[] Arguments => new[] { "css selector" };

}