// See https://aka.ms/new-console-template for more information

using DevGpt.Commands.Pdf;
using DevGpt.Commands.Web.Browser;
using DevGpt.Console;
using DevGpt.Console.Commands;
using DevGpt.Console.Tasks;
using DevGpt.Models.Commands;
using DevGpt.OpenAI;
using DevGpt.OpenAI.RedisCache;
using DevGpt.Taskbased.Projects;
using MyApp;

namespace DevGpt.Taskbased // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var browser = new PlaywrightBrowser();
            var commands = new ICommandBase[]
            {
                new ReadFileCommand(),
                new WriteFileCommand(),
                new SearchFilesCommand(),
                new ShutDownCommand(),
                new ExecuteShellCommand(),
                new DotnetAddReferenceCommand(),
                new AppendFileCommand(),
                new ReadPdfCommand(),

                //new BrowseWebsiteCommand(),
                new BrowserOpenCommand(browser),
                new BrowserGetHtmlCommand(browser),
                new BrowserEnterInputCommand(browser),
                new BrowserClickCommand(browser),
//                new DeterminePageFunctionalityCommand()
            };

            var azureOpenAiClient = new RedisCachingAzureOpenAIClient(new AzureOpenAIClient(),new RedisClient());
            var commandExecutor = new CommandExecutor(commands);
            var responseParser = new ResponseParser();
            var messageHandler = new MessageHandler();
            var developer = new Developer(azureOpenAiClient, commands,commandExecutor,messageHandler,responseParser);
            var engine = new TaskReasoningEngine(azureOpenAiClient, commands,developer,responseParser,messageHandler );
            await engine.SolveObjective(AccountantFactory.GetProject());
        }
    }
}