using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Commands.Magic;
using DevGpt.Commands.Web.Browser;
using DevGpt.Console.Commands.Semantic;
using DevGpt.OpenAI;


namespace DevGpt.Commands.Web.Test
{
    public class BrowserTakeScreenshotCommandTest
    {
        [Fact]
        public async Task ReadWebPageCommand_GetsTheFacts()
        {
            var browser = new PlaywrightBrowser();
            var openPageCommand = new BrowserOpenCommand(browser);
            await openPageCommand.ExecuteAsync("https://www.asr.nl/service/bereken-premie-overlijdensrisicoverzekering");

            var command = new BrowserTakeScreenshotCommand(browser);
            var result = await command.ExecuteAsync(Array.Empty<string>());
        }
    }
}
