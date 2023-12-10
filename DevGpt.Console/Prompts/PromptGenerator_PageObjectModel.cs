using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts
{
    internal class PromptGenerator_PageObjectModel:PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'developer', an AI designed to write code. You are focused on being accurate and complete.\\n" +
            "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "You want to create a page object model in c# for the page at https://www.berekenhet.nl/kalender/weekdag-datum.html and the results page\r\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "1. open the page at https://www.berekenhet.nl/kalender/weekdag-datum.html\n" +
            "1. determine interesting actions on the page and their corresponding selectors\n" +
            "2. creat a xunit project with playwright\n" +
            "2. add a page object model to interact with the page to the project\n" +
            "4. submit the page and create a page object model for the results page and add this as well\n" +
            "5. make sure the project builds and correct any errors\n" +
            "3. Shut down\r\n\r\n" +
            GetGenericPromt();

    }
}
