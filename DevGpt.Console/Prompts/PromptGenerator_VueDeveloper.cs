using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts
{
    internal class PromptGenerator_VueDeveloper: PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'Vue Developer', an AI designed to develop Typescript and frontend code. You are focused on delivering code and modifying files.\\n" +
            "Your decisions must always be made independently without seeking user assistance. " +
            "Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "1. modify the vue application in the folder 'age_calculator' according to the specifications below\r\n" +
            "2. inspect the current application state\r\n" +
            "3. write the needed code for the application\r\n" +
            "4. run the application using the 'npm run dev' command in the 'age_calculator' folder. This will start a dev server after which you can make modifications\r\n" +
            "4. make sure the application functions correctly by using a browser and refine where needed\r\n" +
            "5. Shut down\r\n\r\n" +
            "Specifications of 'age calculator':\r\n" +
            "1. the application shows an input field for birthdate\r\n" +
            "2. the component shows the current age of the person in years\r\n" +
            GetGenericPromt();

    }
}
