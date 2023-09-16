using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using Azure.AI.OpenAI;
using DevGpt.Models.Commands;
using MyApp;
using static System.Net.Mime.MediaTypeNames;

namespace DevGpt.Console.Tasks;

class Developer : IDeveloper
{
    private readonly IAzureOpenAIClient _openAiClient;
    private readonly IList<ICommandBase> _commands;
    private readonly ICommandExecutor _commandExecutor;

    public Developer(IAzureOpenAIClient openAiClient,IList<ICommandBase> commands, ICommandExecutor commandExecutor)
    {
        _openAiClient = openAiClient;
        _commands = commands;
        _commandExecutor = commandExecutor;
    }


    public async Task<DevGptTask> ExecuteTask(string objective, DevGptTask task)
    {
        //var commandsText = string.Join("\n", _commands.Select(c => c.GetHelp()));
        //commandsText += "\n\n";

        //get text from embedded resource
        

        var result = await _commandExecutor.Execute(task.command, task.arguments);
        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine($"result : {result.Result}");
        System.Console.WriteLine($"context : {result.Context}");

        ////create an openai prompt stating the objective and the task
        //var prompt =


        var prompt = "You are a developer that has run the following task, " + Environment.NewLine
                     + $"TASK={JsonSerializer.Serialize(task)} ###END###" +
                     Environment.NewLine
                     + $"RESULT={result.Result}" + Environment.NewLine
                     + $"RESULT_CONTEXT={result.Context}" + Environment.NewLine
                     + $"Interpreted the result. If the task failed define a new task. If the task succeeded update the given task with the result and set the task status to completed" +
                     Environment.NewLine;
        System.Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine(prompt);


        var textResponse = await _openAiClient.CompletePrompt(new List<ChatMessage>{new ChatMessage(ChatRole.User,prompt)});
        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine(textResponse);

        // use a reg to parse the task json from the response
        // TASK={"id":0,"task":"Create a new project","command":"execute_shell","dependent_task_ids":[],"status":"not_started","arguments":["dotnet new console -o myApp --force"],"result":"not yet run"} ###END###

        var taskJson = Regex.Match(textResponse, @"TASK={.*} ###END###").Value;
        taskJson = taskJson.Replace("TASK=", "");
        taskJson = taskJson.Replace("###END###", "").Trim();
        taskJson = taskJson.Replace($"\\", "\\\\");


        var newTask = JsonSerializer.Deserialize<DevGptTask>(taskJson);
        if (newTask.status == "completed")
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Task completed");
            return newTask;
        }
        else
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Task failed. Retring");
            return await ExecuteTask(objective, newTask);
        }
    }

    private string GetEmbeddedResource(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        using (StreamReader reader = new StreamReader(stream))
        {
            string result = reader.ReadToEnd();
            return result;
        }

        
    }
}