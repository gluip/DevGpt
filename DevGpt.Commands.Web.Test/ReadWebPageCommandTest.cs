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
    public class ReadWebPageCommandTest
    {
        [Fact]
        public async Task ReadWebPageCommand_GetsTheFacts()
        {
            var browser = new PlaywrightBrowser();
            var magicFunction = new SimpleFunction(new AzureOpenAIClient());
            var command = new ReadWebPageCommand(browser, magicFunction);
            var result = await command.ExecuteAsync(new[] { "https://nl.wikipedia.org/wiki/Erwin_Olaf" ,"Get Erwin Olaf facts"});


        }
    }
}
