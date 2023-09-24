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
        public override string GetUserPrompt(string commandsText) => "You are journalist ai, an AI designed to investigate and write article. You are focused on being accurate and complete.\\n" +
                                                            "You are asked to write a biography on the dutch photographer Erwin Olaf." +
                                                            "Your decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications." +
                                                            "\\n\\n" +
                                                            "GOALS:\\n\\n\r\n\r\n" +
                                                            "1. search the internet for articles on erwin olaf\r\n" +
                                                            "2. read the articles\r\n" +
                                                            "3. after reading the articles use the information you found to write an article of 500 words\r\n" +
                                                            "4. store the article in olaf.txt\r\n" +
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
                                                            "RESPONSE_FORMAT:\r\n" +
                                                            "{" +
                                                            "\r\n    \"thoughts\": " +
                                                            "      {\r\n   \"text\": \"thought\",\r\n    " +
                                                            "              \"reasoning\": \"reasoning\",\r\n    " +
                                                            "              \"plan\": \"- short bulleted\\\\n- list that conveys\\\\n- long-term plan\",\r\n    " +
                                                            "              \"criticism\": \"constructive self-criticism\",        \r\n    " +
                                                            "\"speak\": \"thoughts summary to say to user\"\r\n     },\r\n    \"command\": {\r\n        \"name\": \"command name\",\r\n        \"args\": [\"arg1\",\"arg2\",..]\r\n    }\r\n}\r\n         \r\nEnsure the response can be parsed is similar to RESPONSE_FORMAT. Make sure endline characters in json values are double encoded using \\\\r\\\\n\r\n";

       
    }
}
