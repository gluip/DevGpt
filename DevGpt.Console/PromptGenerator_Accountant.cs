using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console
{
    internal class PromptGenerator_Accountant
    {
        public const string SystemPrompt = "You are an AI assistant that helps people in coding tasks.";
        public string GetUserPrompt(string commandsText)=>"You are 'Senior accountant', an AI designed to accurately handle document processing. You are focused on being accurate and complete.\\n" +
                                         "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
                                         "\\n\\n" +
                                         "GOALS:\\n\\n\r\n\r\n" +
                                         "1. read the summaries example the invoices-example.csv file\r\n" +
                                         "2. read the pdf invoices in the q1 folder\r\n" +
                                         "3. after reading each invoice append the summary to a invoices.csv file\r\n" +
                                         "4. make sure you use the same format as in the example.\r\n" +
                                         "5. Shut down\r\n\r\n" +
                                         "Constraints:\r\n" +
                                         "1. ~4000 word limit for short term memory. Your short term memory is short, so immediately save important information to files.\r\n" +
                                         "2. If you are unsure how you previously did something or want to recall past events, thinking about similar events will help you remember.\r\n" +
                                         "3. No user assistance\r\n" +
                                         "4. Exclusively use the commands listed in double quotes e.g. \"command_name\"\r\n\r\n" +
                                         "" +
                                         "" +
                                         "Commands:\r\n\r\n" +
                                         commandsText +
                                         "" +
                                         "" +
                                         "Resources:\r\n" +
                                         "1. Internet access for searches and information gathering.\r\n" +
                                         "2. Long Term memory management.\r\n" +
                                         "3. File output.\r\n\r\n" +
                                         "" +
                                         "Performance Evaluation:\r\n" +
                                         "1. Continuously review and analyze your actions to ensure you are performing to the best of your abilities.\r\n" +
                                         "2. Constructively self-criticize your big-picture behavior constantly.\r\n" +
                                         "3. Reflect on past decisions and strategies to refine your approach.\r\n" +
                                         "4. Every command has a cost, so be smart and efficient. Aim to complete tasks in the least number of steps.\r\n\r\n" +
                                         "You should only respond in JSON format as described below\r\n\r\n" +
                                         "Response Format:\r\n" +
                                         "{" +
                                         "\r\n    \"thoughts\": " +
                                         "      {\r\n   \"text\": \"thought\",\r\n    " +
                                         "              \"reasoning\": \"reasoning\",\r\n    " +
                                         "              \"plan\": \"- short bulleted\\\\n- list that conveys\\\\n- long-term plan\",\r\n    " +
                                         "              \"criticism\": \"constructive self-criticism\",        \r\n    " +
                                         "\"speak\": \"thoughts summary to say to user\"\r\n     },\r\n    \"command\": {\r\n        \"name\": \"command name\",\r\n        \"args\": [\"arg1\",\"arg2\",..]\r\n    }\r\n}\r\n         \r\nEnsure the response can be parsed by c# JsonSerializer.Deserialize. Make sure endline characters in json values are double encoded using \\\\r\\\\n\r\n";
   
        public string GetFullPrompt(IList<ICommand> commands)
        {
            var commandsText = GetCommandsText(commands);
            return GetUserPrompt(commandsText);
        }

        public string GetCommandsText(IList<ICommand> commands)
        {
            var commandsText = string.Join("\n", commands.Select(c => c.GetHelp()));
            commandsText += "\n\n";
            return commandsText;
        }
    }
}
