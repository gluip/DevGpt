using Azure.AI.OpenAI;
using Azure;

using System;
using System.ComponentModel.Design.Serialization;
using SharpToken;

namespace DevGpt.Console
{
    public interface IAzureOpenAIClient
    {
        Task<string> CompletePrompt(IList<ChatMessage> allMessages);
    }

    public class AzureOpenAIClient : IAzureOpenAIClient
    {
        
        public AzureOpenAIClient()
        {
        }

        public async Task<string> CompletePrompt(IList<ChatMessage> allMessages)
        {
            //mission statement..first message
            //var messagesToSend = GetMessagesToSend(allMessages);


            // get environment variable 'DevGpt_AzureKey'
            var azureKey = Environment.GetEnvironmentVariable("DevGpt_AzureKey", EnvironmentVariableTarget.User);
            var uri = Environment.GetEnvironmentVariable("DevGpt_AzureUri", EnvironmentVariableTarget.User);


            var client = new OpenAIClient(
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
            foreach (var message in allMessages)
            {
                chatCompletionsOptions.Messages.Add(message);
            }


            //use sharptoken to calculate number of tokens in chatCompletionsOptions.Messages
            var tokenCount = GetTokenCount(chatCompletionsOptions);

            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine($"Estimated {tokenCount} tokens ");


            var completions = await client.GetChatCompletionsAsync(
                deploymentOrModelName: "gpt4",
                chatCompletionsOptions);

            var messageContent = completions.Value.Choices[0].Message.Content;

            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine($"Usage {completions.Value.Usage.TotalTokens} tokens ");

            return messageContent;


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
