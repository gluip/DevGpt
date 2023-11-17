using System.Reflection;
using System.Text.Json;
using Azure.AI.OpenAI;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using DevGpt.OpenAI;

namespace DevGpt.Console.Tasks;

class Developer : IDeveloper
{
    private readonly IDevGptOpenAIClient _openAiClient;
    private readonly IList<ICommandBase> _commands;

    public Developer(IDevGptOpenAIClient openAiClient,IList<ICommandBase> commands)
    {
        _openAiClient = openAiClient;
        _commands = commands;
    }


    public async Task ExecuteTask(string objective, string task)
    {
        var commandsText = string.Join("\n", _commands.Select(c => c.GetHelp()));
        commandsText += "\n\n";

        //get text from embedded resource
        var exampleTask = GetEmbeddedResource("DevGpt.Console.Tasks.Examples.example1.json");
        var workExample = JsonSerializer.Deserialize<WorkExample>(exampleTask);
        
        //create an openai prompt stating the objective and the task
        var prompt=
            "You are an expert task list creation AI tasked with creating a list of tasks as a JSON array, " +
            "considering the ultimate objective of your team: {objective}. "+Environment.NewLine
            + "Create a very short task list based on the objective, the final output of the last task will be provided back to the user. " +
            "Limit tasks types to those that can be completed with the available skills listed below. Task description should be detailed.###" + Environment.NewLine
            + $"AVAILABLE COMMANDS: {commandsText}" + Environment.NewLine
            + "RULES:" + Environment.NewLine
            + "Do not use skills that are not listed." + Environment.NewLine
            + "Always include one skill." + Environment.NewLine
            + "dependent_task_ids should always be an empty array, or an array of numbers representing the task ID it should pull results from." + Environment.NewLine
            + "Make sure all task IDs are in chronological order.###\n" + Environment.NewLine
            + $"EXAMPLE OBJECTIVE={workExample.objective}" + Environment.NewLine
            + $"TASK LIST={JsonSerializer.Serialize(workExample.task_list)}" + Environment.NewLine
            + "OBJECTIVE={objective}" + Environment.NewLine
            + "TASK LIST=";

        var textResponse = _openAiClient.CompletePrompt(new List<DevGptChatMessage>{new DevGptChatMessage(DevGptChatRole.User, prompt) });
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