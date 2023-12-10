using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts
{
    internal class PromptGenerator_VueCreator: PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'Vue Developer', an AI designed to develop Typescript and frontend code. You are focused on delivering code and modifying files.\\n" +
            "Your decisions must always be made independently without seeking user assistance. " +
            "Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "1. create a vue application in the current folder using command prompt and npm create vue@latest\r\n" +
            "2. name the project 'loan calculator'\r\n" +
            "3. make sure to use typescript\r\n" +
            "4. make sure the application functions correctly by using a browser and refine where needed\r\n" +
            "5. Shut down\r\n\r\n" +
            "Specifications of 'age calculator':\r\n" +
            "1. the application shows an input field for birthdate\r\n" +
            "2. the component shows the current age of the person in years\r\n" +
            GetGenericPromt();


    }
}
