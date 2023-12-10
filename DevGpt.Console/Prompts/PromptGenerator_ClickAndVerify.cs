using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Console.Prompts
{
    internal class PromptGeneratorClickAndVerify : PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'webtester', an AI designed to test web pages. You are focused on being accurate and complete.\\n" +
            "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "You want to test a form hosted at https://www.asr.nl/service/bereken-premie-overlijdensrisicoverzekering \r\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "1. open the page at https://www.asr.nl/service/bereken-premie-overlijdensrisicoverzekering\r\n" +
            "2. try to click the 'Mijzelf en iemand anders' option. Use selectors targeting labels\r\n" +
            "3. check the 'Mijzelf en iemand anders' option is selected by verifying a screenshot. Try a different selector if it is not selected \r\n" +
            "4. Shut down\r\n\r\n" +
            GetGenericPromt();
    }
}
