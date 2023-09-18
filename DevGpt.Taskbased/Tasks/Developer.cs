using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using DevGpt.Models.Commands;
using DevGpt.OpenAI;
using MyApp;
using static System.Net.Mime.MediaTypeNames;

namespace DevGpt.Console.Tasks;

class Developer : IDeveloper
{
    private readonly IAzureOpenAIClient _openAiClient;
    private readonly IList<ICommandBase> _commands;
    private readonly ICommandExecutor _commandExecutor;
    private readonly IMessageHandler _messageHandler;
    private readonly IResponseParser _responseParser;

    public Developer(IAzureOpenAIClient openAiClient,IList<ICommandBase> commands, ICommandExecutor commandExecutor,
        IMessageHandler messageHandler,IResponseParser responseParser)
    {
        _openAiClient = openAiClient;
        _commands = commands;
        _commandExecutor = commandExecutor;
        _messageHandler = messageHandler;
        _responseParser = responseParser;
    }


    public async Task ExecuteTask(Project project)
    {
        var taskToRun = project.TaskList.FirstOrDefault(t => t.status == TaskStatus.pending);

        var runResult = await _commandExecutor.Execute(taskToRun.command, taskToRun.arguments);
        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine($"result : {runResult.Result}");
        System.Console.WriteLine($"context : {runResult.Context}");

        ////create an openai prompt stating the objective and the task
        //var prompt =
        var commandsText = string.Join("\n", _commands.Select(c => c.GetHelp()));
        commandsText += "\n\n";


        var prompt = "You are a developer that has run the following task, " + Environment.NewLine
                     + $"TASK={JsonSerializer.Serialize(taskToRun)} ###END###" +
                     Environment.NewLine
                     + $"RESULT={runResult.Result}" + Environment.NewLine
                     + $"RESULT_CONTEXT={runResult.Context}" + Environment.NewLine
                     + "Interpreted the RESULT and RESULT_CONTEXT. Update the TASK_LIST using the result. " +
                     " Update the task status or add/modify tasks when needed. Make sure any \\ is encoded for JSON." + Environment.NewLine
                     + "If the task failed you should try to correct the error by altering the task." + Environment.NewLine
                     + "If the task succeeded you update the result of the task with a summary of the result. Make sure all info relevant to the objective is in the summary. " + Environment.NewLine
                     + "Create additional tasks to the TASK_LIST if you think it is needed to complete the objective. " + Environment.NewLine 
                     + "Make sure all tasks have correct and concrete arguments." + Environment.NewLine
                     + $"OBJECTIVE={project.Objective}" + Environment.NewLine
                     + $"AVAILABLE COMMANDS: {commandsText}" + Environment.NewLine + Environment.NewLine 
                     + "Make sure the TASK_LIST is in chronological order so the first PENDING task should be run next. Always use the 'TASK_LIST=[{task1,task2}] ###END###' format in your response. " + Environment.NewLine
                     + $"AVAILABLE_STATUS_OPTIONS={string.Join(",",Enum.GetNames(typeof(TaskStatus)))}" + Environment.NewLine+
                     $"TASK_LIST={JsonSerializer.Serialize(project.TaskList,new JsonSerializerOptions{WriteIndented = true})}###END###";
                     //Environment.NewLine;
            _messageHandler.HandleMessage(ChatRole.User,prompt);

        

        var textResponse = await _openAiClient.CompletePrompt(new List<ChatMessage> { new ChatMessage(ChatRole.User, prompt) });
        _messageHandler.HandleMessage(ChatRole.Assistant, textResponse);

        project.TaskList = _responseParser.GetTaskList(textResponse); 

        if (project.TaskList.Any(t=>t.status == TaskStatus.pending))
        {
            await ExecuteTask(project);
        }
        else
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("No pending tasks left. Project completed.");
        }
       
    }
}

internal interface IResponseParser
{
    DevGptTask[]? GetTaskList(string textResponse);
}