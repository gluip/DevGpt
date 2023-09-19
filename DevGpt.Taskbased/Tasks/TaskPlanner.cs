﻿using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using DevGpt.Models.Commands;
using DevGpt.OpenAI;
using static System.Net.Mime.MediaTypeNames;

namespace DevGpt.Console.Tasks;

internal interface ITaskPlanner
{
    Task ExecuteTask(Project project);
}

class TaskPlanner : ITaskPlanner
{
    private readonly IAzureOpenAIClient _openAiClient;
    private readonly IList<ICommandBase> _commands;
    private readonly IMessageHandler _messageHandler;
    private readonly IResponseParser _responseParser;

    public TaskPlanner(IAzureOpenAIClient openAiClient,IList<ICommandBase> commands,
        IMessageHandler messageHandler,IResponseParser responseParser)
    {
        _openAiClient = openAiClient;
        _commands = commands;
        _messageHandler = messageHandler;
        _responseParser = responseParser;
    }


    public async Task ExecuteTask(Project project)
    {
        
        ////create an openai prompt stating the objective and the task
        //var prompt =
        var commandsText = string.Join("\n", _commands.Select(c => c.GetHelp()));
        commandsText += "\n\n";


        var prompt = "You are an project manager driven to complete the objective that evaluates a task list provided by a developer. " + Environment.NewLine +
                     "Check if the tasks are sufficient to complete the objective and add/modify tasks when needed. Make sure any \\ is encoded for JSON." + Environment.NewLine
                     + "Make sure all tasks have correct and concrete arguments. Return an updated version of the task list and keep any failed or completed tasks in the list." + Environment.NewLine
                     + $"OBJECTIVE={project.Objective}" + Environment.NewLine
                     + $"AVAILABLE COMMANDS: {Environment.NewLine}{commandsText}" + Environment.NewLine + Environment.NewLine 
                     + "Make sure the TASK_LIST is in chronological order so the first PENDING task should be run next. Always use the 'TASK_LIST=[{task1,task2}] ###END###' format in your response. " + Environment.NewLine
                     + $"AVAILABLE_STATUS_OPTIONS={string.Join(",",Enum.GetNames(typeof(TaskStatus)))}" + Environment.NewLine+
                     $"TASK_LIST={JsonSerializer.Serialize(project.TaskList,new JsonSerializerOptions{WriteIndented = true})}###END###";
                     //Environment.NewLine;
        _messageHandler.HandleMessage(ChatRole.User,prompt);

        

        var textResponse = await _openAiClient.CompletePrompt(new List<ChatMessage> { new ChatMessage(ChatRole.User, prompt) });
        _messageHandler.HandleMessage(ChatRole.Assistant, textResponse);

        project.TaskList = _responseParser.GetTaskList(textResponse); 

        
       
    }
}
