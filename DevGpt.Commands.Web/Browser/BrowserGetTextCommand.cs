using DevGpt.Models.Browser;
using DevGpt.Models.Commands;

namespace DevGpt.Commands.Web.Browser;

public class BrowserGetTextCommand : IAsyncCommand
{
    private readonly IBrowser _browser;

    public BrowserGetTextCommand(IBrowser browser)
    {
        _browser = browser;
    }

    public string Name => "browser_get_text";
    public string Description => "gets the text of the current page in the browser";
    public string[] Arguments => Array.Empty<string>();
    public async Task<string> ExecuteAsync(params string[] args)
    {
        if (args.Length != 0)
        {
            return  $"{Name} requires no arguments";
        }

        try
        {
            return $"{Name} returned text {await _browser.GetPageHtml()}";
        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }
}