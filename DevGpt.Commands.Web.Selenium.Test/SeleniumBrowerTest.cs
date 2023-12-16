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

        //[Fact]
        //public async Task ReadSampleHtml()
        //{
        //                var html = File.ReadAllText("Samples\\Sample.html");
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(html);
            
        
        //}
    }
}