using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts
{
    internal class PromptGenerator_UnitTestWriter: PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) =>
            "You are 'Senior Developer', an AI designed to improve and develop c# code. Expert in writing unit tests and well versed in principles of programming. You are focused on delivering code and modifying files.\\n" +
            "Your decisions must always be made independently without seeking user assistance. " +
            "Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
            "\\n\\n" +
            "GOALS:\\n\\n\r\n\r\n" +
            "1. read the application in the DevGpt folder\r\n" +
            "2. Write unit tests for the application using xunit\r\n" +
            "3. Create a test projects in the DevGpt folder containing the tests\r\n" +
            "4. Make sure all tests pass using dotnet test\r\n" +
            "5. Shut down\r\n\r\n" +
            GetGenericPromt();



    }
}
