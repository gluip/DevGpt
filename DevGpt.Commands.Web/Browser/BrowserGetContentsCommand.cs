using DevGpt.Models.Commands;
using HtmlAgilityPack;

namespace DevGpt.Commands.Web.Browser;

public class BrowserGetHtmlCommand : IAsyncCommand
{
    private readonly IBrowser _browser;

    public BrowserGetHtmlCommand(IBrowser browser)
    {
        _browser = browser;
    }

    public async Task<string> ExecuteAsync(params string[] args)
    {
        if (args.Length != 0)
        {
            return $"{Name} requires no arguments";
        }

        try
        {
            return await _browser.GetPageHtml();

        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }
    public string Name => "browser_get_html";
    public string Description => "gets the html of the current page in the browser";
    public string[] Arguments => Array.Empty<string>();

}