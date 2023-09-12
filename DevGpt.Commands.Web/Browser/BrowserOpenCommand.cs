using DevGpt.Models.Commands;
using HtmlAgilityPack;

namespace DevGpt.Commands.Web.Browser;

public class BrowserOpenCommand : IComplexCommand
{
    private readonly IBrowser _browser;

    public BrowserOpenCommand(IBrowser browser)
    {
        _browser = browser;
    }

    public async Task<ComplexResult> ExecuteAsync(params string[] args)
    {
        if (args.Length != 1)
        {
            return new ComplexResult { Result = $"{Name} requires 1 argument: url" };
        }

        try
        {
            await _browser.OpenPage(args[0]);
            return new ComplexResult
            {
                Result = $"{Name} of '{args[0]}' returned html. Html set in context.",
                Context = await _browser.GetPageHtml()
            };
        }
    
        catch (Exception ex)
        {
            return new ComplexResult { Result = $"{Name} failed with the following error: {ex.Message}" };
        }
    }
    public string Name => "browser_open_page";
    public string Description => "opens the browser for the specific page. returns the page content";
    public string[] Arguments => new[] { "url" };

}