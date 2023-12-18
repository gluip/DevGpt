using System.Reflection;
using System.Text.RegularExpressions;
using DevGpt.Commands.Web.Services;
using DevGpt.Models.OpenAI;
using DevGpt.Models.Utils;
using Microsoft.Playwright;
using static System.Net.Mime.MediaTypeNames;
using IBrowser = DevGpt.Models.Browser.IBrowser;

namespace DevGpt.Commands.Web.Browser;


public class PlaywrightBrowser : IBrowser,IDisposable
{
    private const int DefaultTimeout = 10000;
    private IPage _page;
    private IPlaywright _playwright;
    private Microsoft.Playwright.IBrowser _browser;

    private async Task UpdateDataVisible()
    {
        var script = DevGptResourceReader.GetEmbeddedResource(Assembly.GetExecutingAssembly(),
            "DevGpt.Commands.Web.AnnotateInvisble.js");
        script = "async () => {" + Environment.NewLine+ script + Environment.NewLine + "}";
        await _page.EvaluateAsync(script);
        //execute the script using playwright and wait for 2 seconds
    }

    public async Task OpenPage(string url)
    {
        _playwright = await Playwright.CreateAsync();
        //non headless browser
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        
        _page = await _browser.NewPageAsync();
        var blockedUrls = BrowserHelper.GetBlockedUrls();

        await _page.Context.RouteAsync("**/*", (request) =>
        {
            foreach (var url in blockedUrls)
            {
                if (request.Request.Url.StartsWith(url))
                {
                    request.AbortAsync();
                    return;
                }
            }
            request.ContinueAsync();

        });

        _page.Context.SetDefaultTimeout(DefaultTimeout);
        await _page.GotoAsync(url);
        Thread.Sleep(2000);
    }

    public async Task<string> GetPageHtml()
    {
        await UpdateDataVisible();
        var driverPageSource = await _page.ContentAsync();//InnerHTMLAsync("body");


        var visibleHtml = BrowserHelper.StripInvisibleElements(driverPageSource);
        var cleanHtml = BrowserHelper.CleanHtml(visibleHtml);
        return await Task.FromResult(cleanHtml);
    }

    public Task<string> GetPageText()
    {
        return _page.InnerTextAsync("body");
    }

    public async Task FillAsync(string selector, string value)
    {
        await GetVisibleLocator(selector).FillAsync(value);
    }

    public async Task ClickAsync(string locator)
    {

        await GetVisibleLocator(locator).ClickAsync();
    }

    private ILocator GetVisibleLocator(string locator)
    {
        return _page.Locator($"{locator}:visible");
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