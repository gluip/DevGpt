using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Console.Prompts
{
    internal class PromptGeneratorGeneratorWriteWebTest : PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'test engineer', an AI designed to create automated tests for webpages. You are focused on being accurate and complete.\\n" +
            "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "You want write a test using xunit and Playwright." +
            "You test should do test the form hosted at https://www.berekenhet.nl/kalender/weekdag-datum.html \r\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "0. determine the functionality of the page" + Environment.NewLine +
            "1. create a xunit project called berekenTest\r\n" +
            "2. install playwright nuget  into the project\r\n" +
            "3. check the page and determine the css selectors to use later in the test for both the input fields and the result page\r\n" +
            "4a. write a test that opens the page at https://www.berekenhet.nl/kalender/weekdag-datum.html\r\n" +
            "4b. fills in the form for december 12th 1979\r\n" +
            "4c. asserts the result page shows the day is a wednesday \r\n" +
            "5. run the test and see it passes\r\n" +
            "6. Shut down\r\n\r\n" + GetGenericPromt();

    }
}
