using DevGpt.Models.OpenAI;

namespace DevGpt.Commands.Web.Browser;

public class BrowserCommandBase
{
    private readonly IBrowser _browser;

    protected BrowserCommandBase(IBrowser browser)
    {
        _browser = browser;
    }

    protected async Task<DevGptContextMessage> GetHtmlContextMessage()
    {
        var htmlContextMessage = new DevGptContextMessage("browser_html", "html of page:" + await _browser.GetPageHtml());
        return htmlContextMessage;
    }
}