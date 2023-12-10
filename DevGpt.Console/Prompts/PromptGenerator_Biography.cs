using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts
{
    internal class PromptGenerator_Biography : PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are journalist ai, an AI designed to investigate and write article. You are focused on being accurate and complete.\\n" +
            "You are asked to write a biography on the dutch photographer Erwin Olaf." +
            "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "1. search the internet for articles on erwin olaf\r\n" +
            "2. read the articles\r\n" +
            "3. after reading the articles use the information you found to write an article of 500 words\r\n" +
            "4. store the article in olaf.txt\r\n" +
            "5. Shut down\r\n\r\n" +
            GetGenericPromt();

    }
}
