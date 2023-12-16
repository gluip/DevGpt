namespace DevGpt.Models.Browser;

public interface IBrowser
{
    Task OpenPage(string url);
    Task<string> GetPageHtml();

    Task<string> GetPageText();
    Task FillAsync(string selector, string value);
    Task ClickAsync(string locator);
    Task<string> TakeScreenshot();
    Task<string> TakeBase64Screenshot();
}