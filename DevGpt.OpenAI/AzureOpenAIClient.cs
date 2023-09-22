using Azure;
using Azure.AI.OpenAI;
using SharpToken;

namespace DevGpt.OpenAI
{
    public interface IAzureOpenAIClient
    {
        Task<string> CompletePrompt(IList<ChatMessage> allMessages);
    }

    public class AzureOpenAIClient : IAzureOpenAIClient
    {
        public async Task<string> CompletePrompt(IList<ChatMessage> allMessages)
        {
            //mission statement..first message
            //var messagesToSend = GetMessagesToSend(allMessages);


            // get environment variable 'DevGpt_AzureKey'
            

            var client = GetOpenAiClient();

            // ### If streaming is selected
            var chatCompletionsOptions = new ChatCompletionsOptions
            {
                Temperature = (float)0.5,
                MaxTokens = 1500,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0,

            };
            foreach (var message in allMessages)
            {
                chatCompletionsOptions.Messages.Add(message);
            }


            //use sharptoken to calculate number of tokens in chatCompletionsOptions.Messages
            var tokenCount = GetTokenCount(chatCompletionsOptions);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Estimated {tokenCount} tokens ");


            var completions = await client.GetChatCompletionsAsync(
                deploymentOrModelName: "gpt-4",
                chatCompletionsOptions);

            var messageContent = completions.Value.Choices[0].Message.Content;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Usage {completions.Value.Usage.TotalTokens} tokens ");

            return messageContent;


        }

        private static OpenAIClient GetOpenAiClient()
        {

            // azure version
            var azureKey = Environment.GetEnvironmentVariable("DevGpt_AzureKey", EnvironmentVariableTarget.User);
            var uri = Environment.GetEnvironmentVariable("DevGpt_AzureUri", EnvironmentVariableTarget.User);


            var client = new OpenAIClient(
                new Uri(uri),
                new AzureKeyCredential(azureKey));

            //var openAIKey = Environment.GetEnvironmentVariable("DevGpt_OpenAIKey", EnvironmentVariableTarget.User);
            //var client = new OpenAIClient(openAIKey);
            return client;
        }


        private static int GetTokenCount(ChatCompletionsOptions chatCompletionsOptions)
        {
            var encoding = GptEncoding.GetEncodingForModel("gpt-4");
            var tokenCount = 0;
            foreach (var message in chatCompletionsOptions.Messages)
            {
                tokenCount += encoding.Encode(message.Content).Count;
            }

            return tokenCount;
        }
    }
}
