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

        public TaskReasoningEngine(IAzureOpenAIClient openAiClient, IList<ICommandBase> commands,IDeveloper developer)
        {
            _openAiClient = openAiClient;
            _commands = commands;
            _developer = developer;
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
                + $"EXAMPLE TASK LIST={JsonSerializer.Serialize(project.TaskList,new JsonSerializerOptions{WriteIndented = true})}" + Environment.NewLine;

            // green prompt
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(prompt);

            var response = await _openAiClient.CompletePrompt(new List<ChatMessage>
                { new ChatMessage(ChatRole.User, prompt) });

            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(response);

            //ask developer to solve the first task
            
            while (!project.TaskList.All(c => c.status == "completed"))
            {
                var task = project.TaskList.First(t=>t.status != "completed");
                await _developer.ExecuteTask(project.Objective, task);
            }
        }
    }


    interface IProductOwner
    {
        System.Threading.Tasks.Task PrioritizeTasks(string objective, IList<string> tasks);
    }
    
}
