using DevGpt.Models.Browser;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;

namespace DevGpt.Commands.Web.Browser;

public class BrowserTakeScreenshotCommand : IAsyncMessageCommand
{
    private readonly IBrowser _browser;

    public BrowserTakeScreenshotCommand(IBrowser browser)
    {
        _browser = browser;
    }

    public string[] Arguments => Array.Empty<string>() ;
    public string Description => "Gets a screenshot of the current page in the browser";
    public string Name => "browser_take_screenshot";

    public async Task<IList<DevGptChatMessage>> ExecuteAsync(string[] args)
    {
        var path = await _browser.TakeScreenshot();

        return new[]
        {
            new DevGptToolCallResultMessage(Name,$"Screenshot saved to {path}`")
        };
    }
}