using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace DevGpt.Commands.Web.Browser;

public class PlaywrightBrowser : IBrowser,IDisposable
{
    private IPage _page;
    private IPlaywright _playwright;
    private Microsoft.Playwright.IBrowser _browser;

    
    public async Task<string> OpenPage(string url)
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Webkit.LaunchAsync();
        
        _page = await _browser.NewPageAsync();

        await _page.Context.RouteAsync("**/*", (request) =>
        {
            if (request.Request.Url.StartsWith("https://googleads.") 
                || request.Request.Url.StartsWith("https://www.googleads.") 
                || request.Request.Url.StartsWith("https://pagead2.")
                || request.Request.Url.StartsWith("https://tpc.googlesyndication.com")
                || request.Request.Url.StartsWith("https://www.googletagmanager.com")
                || request.Request.Url.StartsWith("https://www.google-analytics.com")
                || request.Request.Url.StartsWith("https://www.google.com/pagead"))
                request.AbortAsync();
            else
                request.ContinueAsync();

        });

        _page.Context.SetDefaultTimeout(3000);

        await _page.GotoAsync(url);

        return "Page opened";
    }

    public async Task<string> GetPageHtml()
    {
        var html = await _page.InnerHTMLAsync("body");

        // strip comments from html
        html = Regex.Replace(html, "<!--.*?-->", "", RegexOptions.Singleline);
        // strip script tags from html
        html = Regex.Replace(html, "<script.*?</script>", "", RegexOptions.Singleline);
        // strip style tags from html
        html = Regex.Replace(html, "<style.*?</style>", "", RegexOptions.Singleline);
        // strip noscript tags from html
        html = Regex.Replace(html, "<noscript.*?</noscript>", "", RegexOptions.Singleline);
        // strip svg tags from html
        html = Regex.Replace(html, "<svg.*?</svg>", "", RegexOptions.Singleline);
        // strip style attributes from html
        html = Regex.Replace(html, " style=\".*?\"", "", RegexOptions.Singleline);
        // strip class attributes from html
        html = Regex.Replace(html, " class=\".*?\"", "", RegexOptions.Singleline);
        // string whitespace from html
        html = Regex.Replace(html, @"\s+", " ", RegexOptions.Singleline);
        return html;
    }

    public async Task FillAsync(string selector, string value)
    {
        await _page.Locator(selector).FillAsync(value);
    }

    public async Task ClickAsync(string selector)
    {
        await _page.Locator(selector).ClickAsync();
    }

    public void Dispose()
    {
        _playwright.Dispose();
    }
}