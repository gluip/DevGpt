using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Commands.Web.Browser;

namespace DevGpt.Commands.Web.Test.Browser
{
    public class PlaywrightBrowserTest
    {
        [Fact]
        public async Task GetPageHtml()
        {
            var browser = new PlaywrightBrowser();
            await browser.OpenPage("https://www.asr.nl/service/bereken-premie-overlijdensrisicoverzekering");
            var html = await browser.GetPageHtml();
            Assert.Contains("Erwin Olaf", html);
        }
    }
}
