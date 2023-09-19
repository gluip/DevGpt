using DevGpt.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using DevGpt.OpenAI;

namespace DevGpt.Console.Tasks
{
    internal class TaskReasoningEngine
    {

        private readonly IAzureOpenAIClient _openAiClient;
        private readonly IList<ICommandBase> _commands;
        private readonly IDeveloper _developer;
        private readonly IResponseParser _responseParser;
        private readonly IMessageHandler _messageHandler;
        private ITaskPlanner _taskPlanner;

        public TaskReasoningEngine(IAzureOpenAIClient openAiClient, IList<ICommandBase> commands,
            IDeveloper developer,IResponseParser responseParser, IMessageHandler messageHandler,ITaskPlanner taskPlanner)
        {
            _openAiClient = openAiClient;
            _commands = commands;
            _developer = developer;
            _responseParser = responseParser;
            _messageHandler = messageHandler;
            _taskPlanner = taskPlanner;
        }


        public async Task SolveObjective(Project project)
        {

            var commandsText = string.Join("\n", _commands.Select(c => c.GetHelp()));
            commandsText += "\n\n";

            var prompt =
                "You are an expert task list creation AI tasked with creating a list of tasks as a JSON array, " +
                $"considering the ultimate objective of your team: {project.Objective}. " + Environment.NewLine +
                "Create a very short tasklist to work towards the objective. " +
                "Limit tasks types to those that can be completed with the available skills listed below. " +
                "You don't have to complete the objective, only create the tasks of which the arguments are clear. Additional tasks can be added later on." +
                "Task description should be detailed. ###" +
                Environment.NewLine + Environment.NewLine
                + $"AVAILABLE COMMANDS: {commandsText}" + Environment.NewLine
                + "RULES:" + Environment.NewLine
                + "Do not use skills that are not listed." + Environment.NewLine
                + "Always include one skill." + Environment.NewLine
                + "dependent_task_ids should always be an empty array, or an array of numbers representing the task ID it should pull results from." +
                Environment.NewLine
                + "Make sure all task IDs are in chronological order.###\n" + Environment.NewLine
                + $"OBJECTIVE={project.Objective}" + Environment.NewLine
                + $"EXAMPLE TASK_LIST={JsonSerializer.Serialize(project.TaskList,new JsonSerializerOptions{WriteIndented = true})} ###END###" + Environment.NewLine
                +$"TASK_LIST=.....";
            // green prompt
            _messageHandler.HandleMessage(ChatRole.User, prompt);

            var response = await _openAiClient.CompletePrompt(new List<ChatMessage>
                { new ChatMessage(ChatRole.User, prompt) });

            _messageHandler.HandleMessage(ChatRole.Assistant, response);
            

            project.TaskList = _responseParser.GetTaskList(response);
            project.TaskList = project.TaskList.Take(2).ToArray();
            //ask developer to solve the project

            while (project.TaskList.Any(t => t.status == TaskStatus.pending))
            {
                await _developer.ExecuteTask(project);
                await _taskPlanner.ExecuteTask(project);
            }
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("No pending tasks left. Project completed.");
            
            
            
        }
    }


    interface IProductOwner
    {
        System.Threading.Tasks.Task PrioritizeTasks(string objective, IList<string> tasks);
    }
    
}
