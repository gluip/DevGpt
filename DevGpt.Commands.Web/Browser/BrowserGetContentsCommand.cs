using DevGpt.Models.Commands;
using HtmlAgilityPack;

namespace DevGpt.Commands.Web.Browser;

public class BrowserGetHtmlCommand : IComplexCommand
{
    private readonly IBrowser _browser;

    public BrowserGetHtmlCommand(IBrowser browser)
    {
        _browser = browser;
    }

    public async Task<ComplexResult> ExecuteAsync(params string[] args)
    {
        if (args.Length != 0)
        {
            return new ComplexResult { Result = $"{Name} requires no arguments" };
        }

        try
        {
            return new ComplexResult
            {
                Result = $"{Name} returned html. Html set in context",
                Context = await _browser.GetPageHtml()
            };

        }
        catch (Exception ex)
        {
            return new ComplexResult
            {
                Result = $"{Name} failed with the following error: {ex.Message}"
            };
        }
    }
    public string Name => "browser_get_html";
    public string Description => "gets the html of the current page in the browser";
    public string[] Arguments => Array.Empty<string>();

}