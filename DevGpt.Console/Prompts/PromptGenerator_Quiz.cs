using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts
{
    internal class PromptGenerator_Quiz: PromptGeneratorBase
    {
        public override string GetUserPrompt(string commandsText) => "You are a student trying to pass an exam. " +
                                                                     "Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
                                                                     "\\n\\n" +
                                                                     "GOALS:\\n\\n\r\n\r\n" +
                                                                     "1. read the questions in quiz.txt file in the Quiz folder\r\n" +
                                                                     "2. write the answers to all the questions in a answers.txt file in the Quiz folder\r\n" +
                                                                     "3. Make sure all answers are correct\r\n" +
                                                                     "5. Shut down\r\n\r\n" +
                                                                     GetGenericPromt();

    }
}
