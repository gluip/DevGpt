using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Commands.Web.Services;

namespace DevGpt.Commands.Web.Test
{
    public class BrowserHelperTest
    {
        [Fact]
        public void CleanHtml_StripsIrrelevantHtml()
        {
            string htmlContent = File.ReadAllText("Samples/Sample.html");
            var cleanHtml = BrowserHelper.CleanHtml(htmlContent);
            string expectedHtml = File.ReadAllText("Samples/CleanedSample.html");
            Assert.Equal(expectedHtml, cleanHtml);
            //read from sample.html

        }
    }
}
