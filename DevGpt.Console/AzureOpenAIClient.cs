using Azure.AI.OpenAI;
using Azure;


namespace DevGpt.Console
{
    public class AzureOpenAIClient
    {
        public AzureOpenAIClient()
        {
        }

        public async Task<string> CompletePrompt(IList<ChatMessage> chatMessages)
        {
            
            OpenAIClient client = new OpenAIClient(
                new Uri("https://martijnopenaieastus.openai.azure.com/"),
                new AzureKeyCredential("5e7191ba6fac4765b7f9096b61f7aa4a"));

            // ### If streaming is selected
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Temperature = (float)0.5,
                MaxTokens = 500,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0,

            };
            foreach (var message in chatMessages)
            {
                chatCompletionsOptions.Messages.Add(message);
            }
            var completions = await client.GetChatCompletionsAsync(
                deploymentOrModelName: "gpt35",
                chatCompletionsOptions);

            return completions.Value.Choices[0].Message.Content;


        }
    }
}
