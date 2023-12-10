using System.Diagnostics.CodeAnalysis;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using HtmlAgilityPack;

namespace DevGpt.Commands.Web.Browser;

public class BrowserOpenCommand : BrowserCommandBase, IAsyncMessageCommand
{
    private readonly IBrowser _browser;

    public BrowserOpenCommand(IBrowser browser):base(browser)
    {
        _browser = browser;
    }

    public async Task<IList<DevGptChatMessage>> ExecuteAsync(params string[] args)
    {
        if (args.Length != 1)
        {
            return new[]
            {
                new DevGptChatMessage(DevGptChatRole.User,$"{Name} requires 1 argument: url")
            };
        }

        try
        {
            await _browser.OpenPage(args[0]);
            var toolMessage = new DevGptToolCallResultMessage(Name, $"{Name} of '{args[0]}' succeeded. Html set in contextmessage");
            
            
            var htmlContextMessage = await GetHtmlContextMessage();

            //var base64Image = await _browser.TakeBase64Screenshot();
            //var pictureMessage = new DevGptContextMessage("browser_screenshot", new List<DevGptContent>
            //{
            //    new DevGptContent(DevGptContentType.Text, "screenshot of page"),
            //    new DevGptContent(DevGptContentType.ImageUrl, $"data:image/jpeg;base64,{base64Image}")

            //});
            return new DevGptChatMessage[]
            {
                toolMessage,
                htmlContextMessage
            };
        }
        catch (Exception ex)
        {
            return new[]
            {
                new DevGptChatMessage(DevGptChatRole.User, $"{Name} failed with the following error: {ex.Message}")
            };
        }
    }

    public string Name => "browser_open_page";
    public string Description => "opens the browser for the specific page. returns the page html";
    public string[] Arguments => new[] { "url" };

}