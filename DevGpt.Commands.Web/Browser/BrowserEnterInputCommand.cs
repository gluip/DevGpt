using DevGpt.Models.Commands;
using HtmlAgilityPack;

namespace DevGpt.Commands.Web.Browser;

public class BrowserEnterInputCommand : IAsyncCommand
{
    private readonly IBrowser _browser;

    public BrowserEnterInputCommand(IBrowser browser)
    {
        _browser = browser;
    }

    public async Task< string> ExecuteAsync(params string[] args)
    {
        if (args.Length != 2)
        {
            return $"{Name} requires 2 arguments: css selector and value";
        }

        try
        {
            await _browser.FillAsync(args[0], args[1]);
            return "Input entered";
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
    public string Name => "browser_enter_input";
    public string Description => "enters text to a field on a page";
    public string[] Arguments => new[] { "css selector", "value" };

}