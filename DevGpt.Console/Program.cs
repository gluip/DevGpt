// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using DevGpt.Commands;
using DevGpt.Commands.Commands;
using DevGpt.Commands.Magic;
using DevGpt.Commands.Pdf;
using DevGpt.Commands.Web.Browser;
using DevGpt.Commands.Web.Google;
using DevGpt.Commands.Web.Selenium;
using DevGpt.Commands.Web.Semantic;
using DevGpt.Console.Chatmodel;
using DevGpt.Console.Commands;
using DevGpt.Console.Commands.Semantic;
using DevGpt.Console.Logging;
using DevGpt.Console.Prompts;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using DevGpt.Models.Utils;
using DevGpt.OpenAI;
using DevGpt.OpenAI.RedisCache;
using MyApp;

namespace DevGpt.Console // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var browser = new SeleniumBrowser();
            var client = new DotnetOpenAIClient(OpenAiClientType.OpenAI);
            var imageClient = new DotnetOpenAIClient(disableFunctionCalling:true);
            var simpleFunction =new SimpleFunction(client);

            var commands = new List<ICommandBase>
            {
                new GetCurrentDateCommand(),
                new ReadFileCommand(),
                new WriteFileCommand(),
                new SearchFilesCommand(),
                new ShutDownCommand(),
                new ExecuteShellCommand(),
                new DotnetAddReferenceCommand(),
                new ReadPdfCommand(),
                new GoogleSearchCommand(),
                new BrowserOpenCommand(browser),
                new BrowserGetHtmlCommand(browser),
                new BrowserEnterInputCommand(browser),
                new BrowserClickCommand(browser),
                new BrowserTakeScreenshotCommand(browser),
                //new ImageQuestionCommand(imageClient),
                //new ReadWebPageCommand(browser,simpleFunction),
                new ReadWebPageHtmlCommand(browser,simpleFunction)
            };

            var chatHandler = new ChatHandler();
            var commandExecutor = new CommandExecutor(commands);

            System.Console.WriteLine("Hello welcome to DevGpt!");
            //var promptGenerator = new EmbeddedResourcePromptGenerator("Web_TesterHypotheken.txt");


            //var promptGenerator = new PromptGenerator_VueDesigner();
            //var promptGenerator = new PromptGenerator_Biography();
            //var promptGenerator = new PromptGenerator_UnitTestWriter();
            var promptGenerator = new EmbeddedResourcePromptGenerator("Web_TesterHypotheken.txt");
            // var promptGenerator = new PromptGeneratorGeneratorWebTesterORV();
            //var promptGenerator = new PromptGeneratorGeneratorWriteWebTest();
            var fullPrompt = promptGenerator.GetFullPrompt(commands);

            Logger.ConfigurePrompName(promptGenerator.GetType().Name);
            chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.System, fullPrompt));

            while (true)
            {
                var devGptChatResponse = await client.CompletePrompt(chatHandler.GetMessages(), commands);


                chatHandler.AddMessage(devGptChatResponse);

                try
                {
                    WriteReply(devGptChatResponse);
                    if (devGptChatResponse.ToolCalls.Any())
                    {
                        foreach (var toolCall in devGptChatResponse.ToolCalls)
                        {

                            //check if command exists
                            if (commands.All(c => c.Name != toolCall.ToolName))
                            {
                                chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.User,
                                    $"Tool '{toolCall.ToolName}' not found. Proceed to formulate a concrete command."));
                                continue;
                            }

                            //prompt user in a y/n question
                            var resultMessages = await commandExecutor.ExecuteTool(toolCall);

                            foreach (var message in resultMessages)
                            {
                                chatHandler.AddMessage(message);
                            }
                        }
                    }
                    else
                    {
                        chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.User,
                            $"No tool call found. Please make sure to include a tool call in every response"));
                    }
                }
                catch (Exception e)
                {
                    chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.User,
                        $"Unable to parse the response as json. Please provide a proper formatted respose.Error : " +
                        e.Message));
                }
            }
        }

        private static void WriteReply(DevGptChatMessage devGptChatResponse)
        {
            var contentMessage = devGptChatResponse.Content.FirstOrDefault()?.Content;
            if (contentMessage != null)
            {

                try
                {
                    var asistantReply = JsonSerializer.Deserialize<AssitantReply>(contentMessage);
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
                }
                catch (Exception e)
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("********** INVALID RESPONSE *********");

                    System.Console.WriteLine(contentMessage);
                }
            }
            
            foreach (var message in devGptChatResponse.ToolCalls)
            {
                System.Console.WriteLine("Tool: " + message.ToolName);
                System.Console.WriteLine("Args: " + string.Join(" ", message.Arguments));
            }

        }
    }
}