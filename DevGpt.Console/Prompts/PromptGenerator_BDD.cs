using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Console.Prompts
{
    internal class PromptGeneratorGeneratorBDD : PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'test engineer', an AI designed to create automated tests for webpages. You are focused on being accurate and complete.\\n" +
            "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "You want write a test using xunit and Playwright." +
            "You test should do test the form hosted at https://www.berekenhet.nl/kalender/weekdag-datum.html \r\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "1. read the bdd feature in testberekenhet.feature" +
            "2. create a xunit project called berekenTest implementing the feature file using xunit and playwright" +
            "3. run the test and see it passes\r\n" +
            "4. Shut down\r\n\r\n" +
            GetGenericPromt();

    }
}
