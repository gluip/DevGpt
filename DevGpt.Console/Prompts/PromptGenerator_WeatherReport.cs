using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts
{
    internal class PromptGenerator_WeatherReport:PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'journalist', an AI designed to accurately handle document processing. You are focused on being accurate and complete.\\n" +
            "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "1. find out the weather for the coming 5 days in Hilversum using google" +
            "2. write a weather report in dutch of 50 words. Save it in a file called weer.txt\r\n" +
            "3. Shut down\r\n\r\n" + GetGenericPromt();



    }
}
