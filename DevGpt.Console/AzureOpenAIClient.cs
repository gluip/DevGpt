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

            // get environment variable 'DevGpt_AzureKey'
            var azureKey = Environment.GetEnvironmentVariable("DevGpt_AzureKey",EnvironmentVariableTarget.User);
            var uri = Environment.GetEnvironmentVariable("DevGpt_AzureUri",EnvironmentVariableTarget.User);


            OpenAIClient client = new OpenAIClient(
                new Uri(uri),
                new AzureKeyCredential(azureKey));

            // ### If streaming is selected
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Temperature = (float)0.5,
                MaxTokens = 1500,
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
