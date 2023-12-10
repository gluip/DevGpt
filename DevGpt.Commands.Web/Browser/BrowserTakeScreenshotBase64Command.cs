using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using System.Buffers.Text;

namespace DevGpt.Commands.Web.Browser;

public class BrowserTakeScreenshotBase64Command : IAsyncMessageCommand
{
    private readonly IBrowser _browser;

    public BrowserTakeScreenshotBase64Command(IBrowser browser)
    {
        _browser = browser;
    }

    public string[] Arguments => new[] { "" };
    public string Description => "Gets a base 64 screenshot of the current page in the browser";
    public string Name => "browser_take_base64_screenshot";

    public async Task<IList<DevGptChatMessage>> ExecuteAsync(string[] args)
    {
        var base64 = await _browser.TakeBase64Screenshot();
        var uri = $"data:image/jpeg;base64,{base64}";


        return new[]
        {
            new DevGptToolCallResultMessage(Name, new List<DevGptContent>
            {
                new DevGptContent(DevGptContentType.ImageUrl, uri)
            })
        };
    }
}