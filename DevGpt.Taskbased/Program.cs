// See https://aka.ms/new-console-template for more information

using DevGpt.Console;
using DevGpt.Console.Commands;
using DevGpt.Console.Tasks;
using DevGpt.Models.Commands;
using DevGpt.Taskbased.Projects;
using MyApp;

namespace DevGpt.Taskbased // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var commands = new ICommandBase[]
            {
                new ReadFileCommand(),
                new WriteFileCommand(),
                new SearchFilesCommand(),
                new ShutDownCommand(),
                new ExecuteShellCommand(),
                new DotnetAddReferenceCommand(),
                new AppendFileCommand(),
                //new BrowseWebsiteCommand(),
                //new BrowserOpenCommand(browser),
                //new BrowserGetHtmlCommand(browser),
                //new BrowserEnterInputCommand(browser),
                //new BrowserClickCommand(browser),
            };

            System.Console.WriteLine("Hello, World!");
            var azureOpenAiClient = new AzureOpenAIClient();
            var commandExecutor = new CommandExecutor(commands);
            var developer = new Developer(azureOpenAiClient, commands,commandExecutor);
            var engine = new TaskReasoningEngine(azureOpenAiClient, commands,developer );
            await engine.SolveObjective(ConsoleAppFactory.GetConsoleAppProject());
        }
    }
}