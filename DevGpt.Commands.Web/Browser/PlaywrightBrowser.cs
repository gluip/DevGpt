using System.Text.RegularExpressions;
using DevGpt.Models.OpenAI;
using Microsoft.Playwright;
using static System.Net.Mime.MediaTypeNames;

namespace DevGpt.Commands.Web.Browser;

public class PlaywrightBrowser : IBrowser,IDisposable
{
    private const int DefaultTimeout = 10000;
    private IPage _page;
    private IPlaywright _playwright;
    private Microsoft.Playwright.IBrowser _browser;

    
    public async Task<string> OpenPage(string url)
    {
        _playwright = await Playwright.CreateAsync();
        //non headless browser
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        
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

        _page.Context.SetDefaultTimeout(DefaultTimeout);
        
        await _page.GotoAsync(url);

        return "Page opened";
    }

    public async Task<string> GetPageHtml()
    {
        var html = await _page.ContentAsync();//InnerHTMLAsync("body");

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
        // string base64 src attributes from html
        html = Regex.Replace(html, " src=\"data:image.*?\"", "", RegexOptions.Singleline);
        // remove head elements
        html = Regex.Replace(html, "<head.*?</head>", "", RegexOptions.Singleline);
        // remove header element
        html = Regex.Replace(html, "<header.*?</header>", "", RegexOptions.Singleline);
        // remove footer element
        html = Regex.Replace(html, "<footer.*?</footer>", "", RegexOptions.Singleline);
        return html;
    }

    public Task<string> GetPageText()
    {
        return _page.InnerTextAsync("body");
    }

    public async Task FillAsync(string selector, string value)
    {
        await _page.Locator(selector).FillAsync(value);
    }

    public async Task ClickAsync(string locator)
    {
        await _page.Locator(locator).ClickAsync();
    }


    byte[] ResizeImage(byte[] data, double ratio)
    {
        //resize the image by 50% using ImageSharp
        using var image = SixLabors.ImageSharp.Image.Load(data);
        image.Mutate(x => x.Resize((int)(image.Width * ratio), (int)(image.Height * ratio)));
        using var ms = new MemoryStream();
        image.SaveAsJpeg(ms);
        return ms.ToArray();

    }

    public async Task<string> TakeBase64Screenshot()
    {
        var bytes = await _page.ScreenshotAsync(new()
        {
            Type = ScreenshotType.Jpeg
        });

        bytes = ResizeImage(bytes, 0.5);

        return Convert.ToBase64String(bytes);

    }
    public async Task<string> TakeScreenshot()
    {
        var bytes = await _page.ScreenshotAsync(new()
        {
            Type = ScreenshotType.Jpeg
        });
        
        bytes = ResizeImage(bytes, 0.5);

        using var image = SixLabors.ImageSharp.Image.Load(bytes);
        
        //create a screenshot name based on current time including seconds
        var screenshotName =$"screenshot-{DateTime.Now:yyyyMMddHHmmss}.png";
        image.Save(screenshotName);

        return screenshotName;
    }

    public void Dispose()
    {
        _playwright.Dispose();
    }
}