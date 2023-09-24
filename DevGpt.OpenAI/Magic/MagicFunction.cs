using System.Text.Json;
using Azure.AI.OpenAI;
using DevGpt.Models;
using DevGpt.OpenAI;

namespace DevGpt.Commands.Magic;

public class MagicFunction : IMagicFunction
{
    private readonly IAzureOpenAIClient _openAiClient;

    public MagicFunction(IAzureOpenAIClient openAiClient)
    {
        _openAiClient = openAiClient;
    }

    public async Task<T> GetResults<T>(string question, string context,T example)
    {
        var prompt =
            "You are an AI assistant. Try to answer to question of the user in the best possible way. Use exactly the same json format as in ANSWER_EXAMPLE" +
            Environment.NewLine +
            $"QUESTION={question}" + Environment.NewLine +
            $"CONTEXT={context}" + Environment.NewLine +
            $"ANSWER_EXAMPLE={JsonSerializer.Serialize(example)}" + Environment.NewLine +
            "ANSWER=" + Environment.NewLine;

        var response = await _openAiClient.CompletePrompt(new List<ChatMessage> { new ChatMessage(ChatRole.User, prompt) });
        try
        {
            return JsonSerializer.Deserialize<T>(response);

        }
        catch (Exception e)
        {
            Console.WriteLine(response);
            throw;
        }
        //throw new NotImplementedException();
        //return _openAiClient.GetCompletion<T>(prompt);
    }
}
//public class MagicFunction
//{
//    pubic GetResults<SearchResult>("Get the top hits from this html.", html);
//}