using DevGpt.Models.Browser;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using DevGpt.Commands.Web.Services;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V120.Network;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace DevGpt.Commands.Web.Selenium
{
    public class SeleniumBrowser : IBrowser
    {
        private ChromeDriver _driver;

           

        public async Task<string> OpenPage(string url)
        {
            new DriverManager().SetUpDriver(new ChromeConfig()); 
            _driver = new ChromeDriver();
           await ConfigureBlockedUrls();

           _driver.Navigate().GoToUrl(url);




            return await Task.FromResult(_driver.PageSource);
        }

        private async Task ConfigureBlockedUrls()
        {
            var devTools = _driver.GetDevToolsSession();
            var domains = devTools.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V120.DevToolsSessionDomains>();
            await domains.Network.Enable(new EnableCommandSettings());
            
            var blockedURLs = BrowserHelper.GetBlockedUrls().Select(url => $"{url}*").ToArray();
            await domains.Network.SetBlockedURLs(new SetBlockedURLsCommandSettings
            {
                Urls = blockedURLs
            });
        }

        public async Task<string> GetPageHtml()
        {
            return await Task.FromResult(BrowserHelper.CleanHtml(_driver.PageSource));
        }

        public async Task<string> GetPageText()
        {
            return await Task.FromResult(_driver.FindElement(By.TagName("body")).Text);
        }

        public async Task FillAsync(string selector, string value)
        {
            var element = _driver.FindElement(By.CssSelector(selector));
            element.Clear();
            element.SendKeys(value);
            await Task.CompletedTask;
        }

        public async Task ClickAsync(string locator)
        {
            var element = _driver.FindElement(By.CssSelector(locator));
            element.Click();
            await Task.CompletedTask;
        }

        public async Task<string> TakeScreenshot()
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            var screenshotName = $"screenshot-{DateTime.Now:yyyyMMddHHmmss}.png";

            screenshot.SaveAsFile(screenshotName);
            return await Task.FromResult(screenshotName);
        }

        public async Task<string> TakeBase64Screenshot()
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            return await Task.FromResult(screenshot.AsBase64EncodedString);
        }
    }
}
