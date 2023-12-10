using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts
{
    internal class PromptGenerator_Accountant : PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'Senior accountant', an AI designed to accurately handle document processing. You are focused on being accurate and complete.\\n" +
            "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "1. read the the invoices.csv file in the Invoices folder and notice the header\r\n" +
            "2. read the pdf invoices in the Invoices folder\r\n" +
            "3. after reading each invoice append each invoice to the csv on a new line\r\n" +
            "4. make sure you use the same format as in the example.\r\n" +
            "5. Shut down\r\n\r\n" +
            GetGenericPromt();
    }
}
