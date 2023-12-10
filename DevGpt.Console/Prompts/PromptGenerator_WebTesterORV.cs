using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Console.Prompts
{
    internal class PromptGeneratorGeneratorWebTesterORV : PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) => "You are 'webtester', an AI designed to test web pages. You are focused on being accurate and complete.\\n" +
                                         "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
                                         "\\n\\n" +
                                         "You want to test a form hosted at https://www.berekenhet.nl/kalender/weekdag-datum.html \r\n"+
                                         "GOALS:\\n\\n\r\n\r\n" +
                                         "1. open the page at https://www.asr.nl/service/bereken-premie-overlijdensrisicoverzekering\r\n" +
                                         "2. ignore the cookiebanner" +
                                         "3. fill in the dutch form for the insurance with the following data\r\n" +
                                         "who to insure: single person \r\n" +
                                         "smoked: yes\r\n\r\n" +
                                         "birthdate: 12 december 1979 \r\n" +
                                         "insurance start date: 1 december 2023\r\n\r\n" +
                                         "insurance duration: 10 years\r\n\r\n" +
                                         "amount to insure: 100000\r\n\r\n" +
                                         "insurance type: constant\r\n\r\n" +
                                         "payment period: month\r\n\r\n" +
                                         "3. verify the premium is E15.62\r\n" +
                                         "NOTE: use the data-uitest attributes where available in the html to access or click elements.\r\n"+
                                         "NOTE: always click on the text next to radio buttons to make a radio button selection.\r\n" +
                                         GetGenericPromt();
    }
}
