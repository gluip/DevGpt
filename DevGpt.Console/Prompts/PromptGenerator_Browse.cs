using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts
{
    internal class PromptGenerator_Browse : PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'journalist', an AI designed to accurately handle document processing. You are focused on being accurate and complete.\\n" +
            "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "1. read the article at 'https://www.nu.nl/binnenland/6278990/provincie-moet-terug-naar-tekentafel-om-wolf-te-beschieten-met-paintballgeweer.html'\r\n" +
            "2. write a summary of the article in 200 words in english. Save it in a file called wolf.txt\r\n" +
            "3. Shut down\r\n\r\n" +
            GetGenericPromt();

    }
}
