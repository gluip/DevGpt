using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using HtmlAgilityPack;

namespace DevGpt.Commands.Web.Browser;

public class BrowserGetHtmlCommand : IAsyncMessageCommand
{
    private readonly IBrowser _browser;

    public BrowserGetHtmlCommand(IBrowser browser)
    {
        _browser = browser;
    }

    public async Task<IList<DevGptChatMessage>> ExecuteAsync(params string[] args)
    {
        if (args.Length != 0)
        {
            return new List<DevGptChatMessage>
            {
                new DevGptChatMessage(DevGptChatRole.User, $"{Name} requires no arguments")
            };
        }

        try
        {
            var htmlContextMessage = new DevGptContextMessage("browser_html", "html of page:" + await _browser.GetPageHtml());
            var userMessage = new DevGptChatMessage(DevGptChatRole.User, "html set in context");

            return new List<DevGptChatMessage>
            {
                htmlContextMessage,
                userMessage
            };

        }
        catch (Exception ex)
        {
            return new List<DevGptChatMessage>()
            {
                new DevGptChatMessage(DevGptChatRole.User, $"{Name} failed with the following error: {ex.Message}")
            };
        }
    }
    public string Name => "browser_get_html";
    public string Description => "gets the html of the current page in the browser";
    public string[] Arguments => Array.Empty<string>();

}