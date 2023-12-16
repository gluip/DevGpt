using DevGpt.Models.Browser;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Reflection;
using System.Threading.Tasks;
using DevGpt.Commands.Web.Services;
using DevGpt.Models.Utils;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V120.Network;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace DevGpt.Commands.Web.Selenium
{
    public class SeleniumBrowser : IBrowser
    {
        private ChromeDriver _driver;

           

        public async Task OpenPage(string url)
        {
            new DriverManager().SetUpDriver(new ChromeConfig()); 
            _driver = new ChromeDriver(new ChromeOptions{AcceptInsecureCertificates = true});
           await ConfigureBlockedUrls();

           _driver.Navigate().GoToUrl(url);
           //make sure the page is fully loaded

           Thread.Sleep(2000);
        }

        private void UpdateDataVisible()
        {
            var script = DevGptResourceReader.GetEmbeddedResource(typeof(BrowserHelper).Assembly,
                "DevGpt.Commands.Web.AnnotateInvisble.js");

           
            _driver.ExecuteScript(script);
            Thread.Sleep(2000);
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
            UpdateDataVisible();
            var driverPageSource = _driver.PageSource;

            var visibleHtml = BrowserHelper.StripInvisibleElements(driverPageSource);
            var cleanHtml = BrowserHelper.CleanHtml(visibleHtml);
            return await Task.FromResult(cleanHtml);
        }
        
        public async Task<string> GetPageText()
        {
            return await Task.FromResult(_driver.FindElement(By.TagName("body")).Text);
        }

        public async Task FillAsync(string selector, string value)
        {
            var element = _driver.FindElement(GetVisibleLocator(selector));
            element.Clear();
            element.SendKeys(value);
            await Task.CompletedTask;
        }

        public async Task ClickAsync(string selector)
        {
            //modify locator so only data-visible elements are selected
            var locator = GetVisibleLocator(selector);

            var element = _driver.FindElement(locator);
            element.Click();
            await Task.CompletedTask;
        }

        private static By GetVisibleLocator(string locator)
        {
            locator = $"{locator}[data-visible=\"true\"]";
            return By.CssSelector(locator);
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
