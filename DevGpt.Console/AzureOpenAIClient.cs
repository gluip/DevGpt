using Azure.AI.OpenAI;
using Azure;


namespace DevGpt.Console
{
    public class AzureOpenAIClient
    {
        public AzureOpenAIClient()
        {
        }

        public async Task<string> CompletePrompt(string prompt)
        {
            
            OpenAIClient client = new OpenAIClient(
                new Uri("https://martijnopenaieastus.openai.azure.com/"),
                new AzureKeyCredential("5e7191ba6fac4765b7f9096b61f7aa4a"));

            // ### If streaming is selected
            var completions = await client.GetChatCompletionsAsync(
                deploymentOrModelName: "gpt35",
                new ChatCompletionsOptions()
                {
                    Messages = {
                        new ChatMessage(ChatRole.System,PromptGenerator.SystemPrompt),
                        new ChatMessage(ChatRole.System, PromptGenerator.UserPrompt)
                    },
                    Temperature = (float)0.5,
                    MaxTokens = 500,
                    NucleusSamplingFactor = (float)0.95,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,

                });

            return completions.Value.Choices[0].Message.Content;


        }
    }
}
