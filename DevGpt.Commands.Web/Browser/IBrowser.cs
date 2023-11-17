namespace DevGpt.Commands.Web.Browser;

public interface IBrowser
{
    Task<string> OpenPage(string url);
    Task<string> GetPageHtml();

    Task<string> GetPageText();
    Task FillAsync(string selector, string value);
    Task ClickAsync(string selector);
    Task TakeScreenshot();
}