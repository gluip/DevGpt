﻿using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Transactions;
using DevGpt.Commands.Functions;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using DevGpt.OpenAIDotnet;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;

namespace DevGpt.OpenAI
{
   

    public class DotnetOpenAIClient : IDevGptOpenAIClient
    {
        private double? totalRunCosts = 0;
        
        public async Task<string> CompletePrompt(IList<DevGptChatMessage> allMessages,
            IList<ICommandBase> commands = null)
        {
            //mission statement..first message
            //var messagesToSend = GetMessagesToSend(allMessages);


            // get environment variable 'DevGpt_AzureKey'
            

            var client = GetOpenAiClient();
            double? temp = 0.5;
            // ### If streaming is selected

            var messages = new List<Message>();
            foreach (var message in allMessages)
            {
                messages.Add(DotnetChatMessageMapper.Map(message));
            }

            var model = allMessages.Any(m => m.Content.Any(c => c.ContentType == DevGptContentType.ImageUrl))
                ? "gpt-4-vision-preview"
                : "gpt-4-1106-preview";
            
            var adapters = commands?.Select(c => new FunctionAdapter(c)).ToList();
            IEnumerable<Tool>? tools = adapters?.Select(a => (Tool)a.GetFunction()).ToList();
            

            var chatRequest = new ChatRequest(messages,tools: tools ?? Enumerable.Empty<Tool>(),toolChoice:"auto"
                ,model:
                model, temperature:0.5,maxTokens:1500,responseFormat:ChatResponseFormat.Text);
            

            Console.ForegroundColor = ConsoleColor.Blue;


            var completions = await client.GetCompletionAsync(chatRequest);

            var messageContent = completions.FirstChoice.Message.Content;

            Console.ForegroundColor = ConsoleColor.Blue;
            var inputCost = (completions.Usage.PromptTokens * 0.01 / 1000) ;
            var outputCost = (completions.Usage.CompletionTokens * 0.03 / 1000) ;
            totalRunCosts += inputCost + outputCost;
            Console.WriteLine($"** Usage {completions.Usage.TotalTokens} tokens **. Cost of prompt {inputCost+outputCost:C}");
            Console.WriteLine($"** Total costs {totalRunCosts:C}");

            return System.Text.Json.JsonSerializer.Deserialize<string>(messageContent);
            //return messageContent.ToString();


        }

        private static ChatEndpoint GetOpenAiClient()
        {
            var useAzure = false;

            var openAIKey = Environment.GetEnvironmentVariable("DevGpt_OpenAIKey", EnvironmentVariableTarget.User); 
            return new OpenAIClient(openAIKey).ChatEndpoint;


        }


       
    }
}
