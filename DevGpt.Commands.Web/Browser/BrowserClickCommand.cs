using DevGpt.Models.Browser;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using HtmlAgilityPack;

namespace DevGpt.Commands.Web.Browser;

public class BrowserClickCommand : BrowserCommandBase, IAsyncMessageCommand
{
    private readonly IBrowser _browser;

    public BrowserClickCommand(IBrowser browser):base(browser)
    {
        _browser = browser;
    }

    public async Task<IList<DevGptChatMessage>> ExecuteAsync(params string[] args)
    {
        if (args.Length != 1)
        {
            return new List<DevGptChatMessage>()
            {
                new DevGptToolCallResultMessage(Name, $"{Name} requires 1 argument: playwright locator")
            };
        }

        try
        {
            await _browser.ClickAsync(args[0]);
            var userMessage = new DevGptToolCallResultMessage(Name, $"Element {args[0]} clicked. browser_html updated");

            return new DevGptChatMessage[]
            {
                await GetHtmlContextMessage(),
                userMessage
            };
        }
        catch (TimeoutException)
        {
            return new List<DevGptChatMessage>()
            {
                new DevGptToolCallResultMessage(Name, $"No element found for selection")
            };
        }
        catch (Exception e)
        {
            return new List<DevGptChatMessage>()
            {
                new DevGptToolCallResultMessage(Name, $"{Name} failed with the following error: {e.Message}")
            };
        }
    }
    public string Name => "browser_click";
    public string Description => "clicks on a element on a page";
    public string[] Arguments => new[] { "css selector" };

}