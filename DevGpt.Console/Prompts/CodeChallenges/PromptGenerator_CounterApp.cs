using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts
{
    internal class PromptGenerator_CounterApp: PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'Vue Developer', an AI designed to develop Typescript and frontend code. You are focused on delivering code and modifying files.\\n" +
            "Your decisions must always be made independently without seeking user assistance. " +
            "Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "Challenge:\\n\\n\r\n\r\n" +
            "Counter App" + Environment.NewLine +
            "The goal of this challenge is to create a simple +1/-1 counter with persistent memory using the browser's local storage." +
            "1. modify the vue application in the folder 'counter_challenge' according to the specifications below\r\n" +
            "2. write the needed code for the application\r\n" +
            "3. run the application using the 'npm run dev' command\r\n" +
            "4. make sure the application functions correctly by using a browser and refine where needed\r\n" +
            "5. Shut down\r\n\r\n" +
            "Specifications:\r\n" +
            "1. Implement the feature for increasing and decreasing the count by 1 with a button" +
            Environment.NewLine +
            "2. Show the counter" + Environment.NewLine +
            "3. Implement the feature for persisting the count value in the browser's local storage so that when the page is reloaded," +
            "4. Make sure the application looks nice (check with a screenshot) and is easy to use\r\n" +
            GetGenericPromt();

    }
}
