using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using HtmlAgilityPack;

namespace DevGpt.Commands.Web.Browser;

public class BrowserEnterInputCommand :BrowserCommandBase, IAsyncMessageCommand
{
    private readonly IBrowser _browser;

    public BrowserEnterInputCommand(IBrowser browser):base(browser)
    {
        _browser = browser;
    }

    public async Task<IList<DevGptChatMessage>> ExecuteAsync(params string[] args)
    {
        if (args.Length != 2)
        {
            return new List<DevGptChatMessage>()
            {
                new DevGptChatMessage(DevGptChatRole.User, $"{Name} requires 2 arguments: css selector and value")
            };
        }

        try
        {
            await _browser.FillAsync(args[0], args[1]);
            var contextMessage = await GetHtmlContextMessage();
            var userMessage = new DevGptChatMessage(DevGptChatRole.User, $"Input '{args[1]}' entered at selector '{args[0]}'");
            return new[]
            {
                contextMessage,
                userMessage
            };
        }
        catch (TimeoutException e)
        {
            return new List<DevGptChatMessage>()
            {
                new DevGptChatMessage(DevGptChatRole.User, $"No element found for selection")
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
    public string Name => "browser_enter_input";
    public string Description => "enters text to a field on a page";
    public string[] Arguments => new[] { "css selector", "value" };

}