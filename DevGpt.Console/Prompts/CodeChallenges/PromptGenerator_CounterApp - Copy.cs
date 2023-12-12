using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts
{
    internal class PromptGenerator_ChartingApp: PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'Vue Developer', an AI designed to develop Typescript and frontend code. You are focused on delivering code and modifying files.\\n" +
            "Your decisions must always be made independently without seeking user assistance. " +
            "Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "Challenge:\\n\\n\r\n\r\n" +
            "Counter App" + Environment.NewLine +
            "The goal of this challenge is to create a vue app with a linechart showing a mortage of 30 years." +Environment.NewLine+

            "1. modify the vue application in the folder 'charting_challenge' according to the specifications below\r\n" +
            "2. write the needed code for the application\r\n" +
            "3. run the application using the 'npm run dev' command\r\n" +
            "4. make sure the application functions correctly an shows a chart by using a browser and fix any errors\r\n" +
            "5. Shut down\r\n\r\n" +
            "Specifications:\r\n" +
            "1. The application should have the following input fields: mortgage amount, interest rate, term (years)" +
            Environment.NewLine +
            "2. The application should calculate and show the monthly payment to finish the mortgage in term. It should use an annuity calculation." + Environment.NewLine +
            "3. The application should show a linechart showing the progress of the mortgage payments through the years." + Environment.NewLine+
            "4. The application should use the following defaults: amount : 100000, interest rate 4%, term 30 years"+Environment.NewLine+
            "5. Make sure the application looks nice (check with a screenshot) and is easy to use\r\n" +Environment.NewLine+
            "6. Make sure the application shows a correct chart.\r\n" +
            GetGenericPromt();

    }
}
