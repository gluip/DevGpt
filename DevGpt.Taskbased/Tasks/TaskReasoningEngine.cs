using DevGpt.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.AI.OpenAI;

namespace DevGpt.Console.Tasks
{
    internal class TaskReasoningEngine
    {

        private readonly IAzureOpenAIClient _openAiClient;
        private readonly IList<ICommandBase> _commands;
        private readonly IDeveloper _developer;
        private readonly IResponseParser _responseParser;
        private readonly IMessageHandler _messageHandler;

        public TaskReasoningEngine(IAzureOpenAIClient openAiClient, IList<ICommandBase> commands,
            IDeveloper developer,IResponseParser responseParser, IMessageHandler messageHandler)
        {
            _openAiClient = openAiClient;
            _commands = commands;
            _developer = developer;
            _responseParser = responseParser;
            _messageHandler = messageHandler;
        }


        public async Task SolveObjective(Project project)
        {

            var commandsText = string.Join("\n", _commands.Select(c => c.GetHelp()));
            commandsText += "\n\n";

            var prompt =
                "You are an expert task list creation AI tasked with creating a list of tasks as a JSON array, " +
                "considering the ultimate objective of your team: {objective}. " + Environment.NewLine
                + "Create a very short task list based on the objective, the final output of the last task will be provided back to the user. " +
                "Limit tasks types to those that can be completed with the available skills listed below. " +
                "Task description should be detailed. Add a complete task list to complete the objective###" +
                Environment.NewLine
                + $"AVAILABLE COMMANDS: {commandsText}" + Environment.NewLine
                + "RULES:" + Environment.NewLine
                + "Do not use skills that are not listed." + Environment.NewLine
                + "Always include one skill." + Environment.NewLine
                + "dependent_task_ids should always be an empty array, or an array of numbers representing the task ID it should pull results from." +
                Environment.NewLine
                + "Make sure all task IDs are in chronological order.###\n" + Environment.NewLine
                + $"OBJECTIVE={project.Objective}" + Environment.NewLine
                + $"EXAMPLE TASK_LIST={JsonSerializer.Serialize(project.TaskList,new JsonSerializerOptions{WriteIndented = true})} ###END###" + Environment.NewLine
                +$"TASK_LIST=..... ###END###";
            // green prompt
            _messageHandler.HandleMessage(ChatRole.User, prompt);

            var response = await _openAiClient.CompletePrompt(new List<ChatMessage>
                { new ChatMessage(ChatRole.User, prompt) });

            _messageHandler.HandleMessage(ChatRole.Assistant, response);
            

            project.TaskList = _responseParser.GetTaskList(response);

            //ask developer to solve the project
            await _developer.ExecuteTask(project);

            
        }
    }


    interface IProductOwner
    {
        System.Threading.Tasks.Task PrioritizeTasks(string objective, IList<string> tasks);
    }
    
}
