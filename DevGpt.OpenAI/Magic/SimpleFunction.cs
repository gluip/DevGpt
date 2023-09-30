using Azure.AI.OpenAI;
using DevGpt.OpenAI;

namespace DevGpt.Commands.Magic;

public interface ISimpleFunction
{
    Task<string> GetResults(string question, string context);
}
public class SimpleFunction : ISimpleFunction
{
    private readonly IAzureOpenAIClient _openAiClient;

    public SimpleFunction(IAzureOpenAIClient openAiClient)
    {
        _openAiClient = openAiClient;
    }

    public async Task<string> GetResults(string question, string context)
    {
        var prompt =
            $"{question} context: {context}";

        return await _openAiClient.CompletePrompt(new List<ChatMessage> { new ChatMessage(ChatRole.User, prompt) });


    }
}