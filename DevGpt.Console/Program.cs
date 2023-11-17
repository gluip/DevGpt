// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Azure.AI.OpenAI;
using DevGpt.Commands.Magic;
using DevGpt.Commands.Pdf;
using DevGpt.Commands.Web.Browser;
using DevGpt.Commands.Web.Google;
using DevGpt.Commands.Web.Semantic;
using DevGpt.Console.Chatmodel;
using DevGpt.Console.Commands;
using DevGpt.Console.Commands.Semantic;
using DevGpt.Console.Logging;
using DevGpt.Console.Prompts;
using DevGpt.Console.Services;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using DevGpt.OpenAI;
using DevGpt.OpenAI.RedisCache;
using MyApp;

namespace DevGpt.Console // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static ResponseCleaner _responseCleaner = new ResponseCleaner();

        static async Task Main(string[] args)
        {


            var browser = new PlaywrightBrowser();
            var client = new RedisCachingAzureOpenAIClient(new AzureOpenAIClient(), new RedisClient());
            //var client = new AzureOpenAIClient();
            var simpleFunction = new SimpleFunction(client);
            var commands = new ICommandBase[]
            {
                new ReadFileCommand(),
                new WriteFileCommand(),
                new SearchFilesCommand(),
                new ShutDownCommand(),
                new ExecuteShellCommand(),
                new DotnetAddReferenceCommand(),
                new ReadPdfCommand(),
                new AppendFileCommand(),
                new GoogleSearchCommand(),
                //new BrowseWebsiteCommand(),
                new BrowserOpenCommand(browser),
                new BrowserGetHtmlCommand(browser),
                new BrowserEnterInputCommand(browser),
                new BrowserClickCommand(browser),
                //new ReadWebPageCommand(browser,simpleFunction),
                //new ReadWebPageHtmlCommand(browser,simpleFunction)
            };

            
            //var client = new AzureOpenAIClient();
            var chatHandler = new ChatHandler();
            var commandExecutor = new CommandExecutor(commands);

            System.Console.WriteLine("Hello welcome to DevGpt!");
            //var prompt = "You are 'Senior Developer', an AI designed to improve and develop c# code. Expert in writing unit tests and well versed in principles of programming. You are focused on delivering code and modifying files.\\nYour decisions must always be made independently without seeking user assistance. Play to your strengths as an LLM and pursue simple strategies with no legal complications.\\n\\nGOALS:\\n\\n\r\n\r\n1. read the calculator  class of the Sampleconsole application provided to you in the workspace\r\n2. Rewrite the static calculator class to a non-static class and adapt usages in program.cs\r\n3. Write unit tests to the calculator class using xunit\r\n4. Write the unit tests to a file in the Sampleconsole.Tests project in your workspace\r\n5. Shut down\r\n\r\nConstraints:\r\n1. ~4000 word limit for short term memory. Your short term memory is short, so immediately save important information to files.\r\n2. If you are unsure how you previously did something or want to recall past events, thinking about similar events will help you remember.\r\n3. No user assistance\r\n4. Exclusively use the commands listed in double quotes e.g. \"command name\"\r\n\r\nCommands:\r\n\r\n1. Google Search: \"google\", args: \"input\": \"<search>\"\r\n2. Browse Website: \"browse_website\", args: \"url\": \"<url>\", \"question\": \"<what_you_want_to_find_on_website>\"\r\n3. Write to file: \"write_to_file\", args: \"file\": \"<file>\", \"text\": \"<text>\"\r\n4. Read file: \"read_file\", args: \"file\": \"<file>\"\r\n5. Append to file: \"append_to_file\", args: \"file\": \"<file>\", \"text\": \"<text>\"\r\n6. Delete file: \"delete_file\", args: \"file\": \"<file>\"\r\n7. Search Files: \"search_files\", args: \"directory\": \"<directory>\"\r\n8. Evaluate Code: \"evaluate_code\", args: \"code\": \"<full_code_string>\"\r\n9. Get Improved Code: \"improve_code\", args: \"suggestions\": \"<list_of_suggestions>\", \"code\": \"<full_code_string>\"\r\n10. Write Tests: \"write_tests\", args: \"code\": \"<full_code_string>\", \"focus\": \"<list_of_focus_areas>\"\r\n11. Do Nothing: \"do_nothing\", args:\r\n12. Task Complete (Shutdown): \"task_complete\", args: \"reason\": \"<reason>\"\r\n\r\nResources:\r\n1. Internet access for searches and information gathering.\r\n2. Long Term memory management.\r\n3. File output.\r\n\r\nPerformance Evaluation:\r\n1. Continuously review and analyze your actions to ensure you are performing to the best of your abilities.\r\n2. Constructively self-criticize your big-picture behavior constantly.\r\n3. Reflect on past decisions and strategies to refine your approach.\r\n4. Every command has a cost, so be smart and efficient. Aim to complete tasks in the least number of steps.\r\n\r\nYou should only respond in JSON format as described below\r\n\r\nResponse Format:\r\n{\r\n    \"thoughts\": {\r\n    \"text\": \"thought\",\r\n    \"reasoning\": \"reasoning\",\r\n    \"plan\": \"- short bulleted\\\\n- list that conveys\\\\n- long-term plan\",\r\n    \"criticism\": \"constructive self-criticism\",        \r\n    \"speak\": \"thoughts summary to say to user\"\r\n     },\r\n    \"command\": {\r\n        \"name\": \"command name\",\r\n        \"args\": {\r\n               \"arg name\": \"value\"       \r\n         }\r\n    }\r\n}\r\n         \r\nEnsure the response can be parsed by Python json.loads'";
            //var promptGenerator = new PromptGenerator_Browse();
            //IPromptGenerator promptGenerator = new PromptGeneratorGeneratorBDD();
            var promptGenerator = new PromptGeneratorGeneratorWebTesterORV();
            //var promptGenerator = new PromptGenerator_Biography();
            //var promptGenerator = new PromptGenerator_UnitTestWriter();
            //var promptGenerator = new PromptGenerator_Quiz();
            var fullPrompt = promptGenerator.GetFullPrompt(commands);

            Logger.ConfigurePrompName(promptGenerator.GetType().Name);
            //chatHandler.AddMessage(new ChatMessage(ChatRole.System, PromptGenerator_Accountant.SystemPrompt));
            chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.System, fullPrompt));

            while (true)
            {
                var assistantReplyText = await client.CompletePrompt(chatHandler.GetMessages());

                assistantReplyText = _responseCleaner.GetTextBetweenBraces(assistantReplyText);
                var cleanedReplyText = _responseCleaner.CleanJsonResponse(assistantReplyText);
                var balancedText = _responseCleaner.BalanceBraces(cleanedReplyText);

                chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.Assistant, balancedText));

                try
                {
                    var asistantReply = JsonSerializer.Deserialize<AssitantReply>(balancedText);
                    WriteReply(asistantReply);
                    if (asistantReply.command != null)
                    {
                        //check if command exists
                        if (commands.All(c => c.Name != asistantReply.command.name))
                        {
                            chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.User, $"Command '{asistantReply.command.name}' not found. Proceed to formulate a concrete command."));
                            continue;
                        }



                        //prompt user in a y/n question
                        Command command = asistantReply.command;
                        var result = await commandExecutor.Execute(command.name, command.args.ToArray());

                        chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.User, result));
                        
                    }
                }
                catch (Exception e)
                {
                    chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.User, $"Unable to parse the response as json. Please provide a proper formatted respose.Error : "+e.Message));

                }

            }
        }

        private static void WriteReply(AssitantReply asistantReply)
        {
            //write reply using colors
            if (asistantReply.thoughts != null)
            {
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine(asistantReply.thoughts.text);
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine(asistantReply.thoughts.reasoning);
                System.Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine(asistantReply.thoughts.plan);
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(asistantReply.thoughts.criticism);
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine(asistantReply.thoughts.speak);
                System.Console.ForegroundColor = ConsoleColor.White;

            }

            if (asistantReply.command != null)
            {
                System.Console.WriteLine("Command: " + asistantReply.command.name);
                System.Console.WriteLine("Args: " + string.Join(" ", asistantReply.command.args));
            }

        }
    }
}