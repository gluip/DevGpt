using DevGpt.Models.Browser;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace DevGpt.Commands.Web.Selenium
{
    public class SeleniumBrowser : IBrowser
    {
        private IWebDriver driver;

        public SeleniumBrowser()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
        }

        public async Task<string> OpenPage(string url)
        {
            driver.Navigate().GoToUrl(url);
            return await Task.FromResult(driver.PageSource);
        }

        public async Task<string> GetPageHtml()
        {
            return await Task.FromResult(driver.PageSource);
        }

        public async Task<string> GetPageText()
        {
            return await Task.FromResult(driver.FindElement(By.TagName("body")).Text);
        }

        public async Task FillAsync(string selector, string value)
        {
            var element = driver.FindElement(By.CssSelector(selector));
            element.Clear();
            element.SendKeys(value);
            await Task.CompletedTask;
        }

        public async Task ClickAsync(string locator)
        {
            var element = driver.FindElement(By.CssSelector(locator));
            element.Click();
            await Task.CompletedTask;
        }

        public async Task<string> TakeScreenshot()
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var screenshotName = $"screenshot-{DateTime.Now:yyyyMMddHHmmss}.png";

            screenshot.SaveAsFile(screenshotName);
            return await Task.FromResult(screenshotName);
        }

        public async Task<string> TakeBase64Screenshot()
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            return await Task.FromResult(screenshot.AsBase64EncodedString);
        }
    }
}
