using System.Xml;

namespace DevGpt.Commands.Web.Selenium.Test
{
    public class SeleniumBrowerTest
    {
        [Fact]
        public async Task GetHtml_GetsHtml()
        {
            var browser = new SeleniumBrowser();
            await browser.OpenPage("https://www.asr.nl/service/bereken-premie-overlijdensrisicoverzekering");
            var visibleHtml = await browser.GetPageHtml();

        }

        [Fact]
        public async Task ClickOnElementWithInvalidId()
        {
            var browser = new SeleniumBrowser();
            await browser.OpenPage(
                "https://www.asr.nl/hypotheek/welthuis-startershypotheek/maximale-hypotheek-berekenen");
            await browser.ClickAsync("input#3cdfaba5-33b2-4c25-857f-cd9761e2c756");

        }

        [Fact]
        public async Task SelectorByInValid()
        {
            var browser = new SeleniumBrowser();
            var id = "#40b5ce19-5633-469a-a00a-5a485d90bfcf";
            await browser.OpenPage(
                "https://www.asr.nl/hypotheek/welthuis-startershypotheek/maximale-hypotheek-berekenen");
            await browser.ClickAsync(id);
        }

        [Fact]
        public void EscapeSelectorTest()
        {
            var escaped = SeleniumBrowser.ConvertIdToAttributeSelector("input#3cdfaba5-33b2-4c25-857f-cd9761e2c756");
            Assert.Equal("input[id=\"3cdfaba5-33b2-4c25-857f-cd9761e2c756\"]", escaped);
        
        }
    

    //[Fact]
        //public async Task ReadSampleHtml()
        //{
        //                var html = File.ReadAllText("Samples\\Sample.html");
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(html);
            
        
        //}
    }
}