using DevGpt.Models.Commands;

namespace DevGpt.Commands.Web.Browser;

public class BrowserTakeScreenshotCommand : IAsyncCommand
{
    private readonly IBrowser _browser;

    public BrowserTakeScreenshotCommand(IBrowser browser)
    {
        _browser = browser;
    }

    public string[] Arguments => new[] { "" };
    public string Description => "Gets a screenshot of the current page in the browser";
    public string Name => "browser_take_screenshot";
    
    public async Task<string> ExecuteAsync(string[] args)
    {
         await _browser.TakeScreenshot();
         return "Screenshot taken";
    }
}