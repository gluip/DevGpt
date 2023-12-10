using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Console.Prompts
{
    internal class PromptGeneratorGeneratorWebTesterHighlevel : PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'webtester', an AI designed to test web pages. You are focused on being accurate and complete.\\n" +
            "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "You want to the page hosted at https://www.berekenhet.nl/kalender/weekdag-datum.html \r\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "1. inspect the functionality in https://www.berekenhet.nl/kalender/weekdag-datum.html\r\n" +
            "1. create a project in a folder called 'weekdays' testing the webpage using xunit and playwright\r\n" +
            "1. make sure the project compiles and all tests pass\r\n" +
            "3. Shut down\r\n\r\n" +
            GetGenericPromt();
    }
}
