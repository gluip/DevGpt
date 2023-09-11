using Azure.AI.OpenAI;
using Azure;
using DevGpt.Console.Logging;

using System;
using System.ComponentModel.Design.Serialization;
using DevGpt.Memory;
using SharpToken;

namespace DevGpt.Console
{
    public class AzureOpenAIClient
    {
        private readonly IMemoryManager _memoryManager;

        public AzureOpenAIClient(IMemoryManager memoryManager)
        {
            _memoryManager = memoryManager;
        }

        public async Task<string> CompletePrompt(IList<ChatMessage> allMessages)
        {
            //mission statement..first message
            var messagesToSend = GetMessagesToSend(allMessages);


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
            foreach (var message in messagesToSend)
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
            Logger.LogReply(messageContent);

            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine($"Usage {completions.Value.Usage.TotalTokens} tokens ");

            return messageContent;


        }

        private IList<ChatMessage> GetMessagesToSend(IList<ChatMessage> allMessages)
        {
            // no too many messages
            if (allMessages.Count <= 5)
            {
                return allMessages;
            }

            var lastMessage = allMessages.Last();

            var messagesToSend = new List<ChatMessage> { allMessages.First() };

            //latest message

            //relevant x messages
            var relevantMessages = _memoryManager.RetrieveRelevantMessages(lastMessage.Content, 5);

            foreach (var message in allMessages.Skip(1).Take(allMessages.Count - 2))
            {
                if (relevantMessages.Any(rm => rm == message.Content && message.Content != lastMessage.Content))
                {
                    messagesToSend.Add(message);
                }
            }

            messagesToSend.Add(lastMessage);
            return messagesToSend;

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
