using System.ComponentModel.Design;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Transactions;
using DevGpt.Commands.Functions;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using DevGpt.OpenAI.RedisCache;
using DevGpt.OpenAIDotnet;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;

namespace DevGpt.OpenAI
{
    public class DotnetOpenAIClient : IDevGptOpenAIClient
    {
        public DotnetOpenAIClient(OpenAiClientType clientType = OpenAiClientType.AzureOpenAI,bool disableFunctionCalling= false)
        {
            _clientType = clientType;
            _disableFunctionCalling = disableFunctionCalling;
        }

        private double? totalRunCosts = 0;
        private IDictionary<DevGptChatMessage, Message> messageLookup = new Dictionary<DevGptChatMessage, Message>();
        private readonly OpenAiClientType _clientType;
        private readonly bool _disableFunctionCalling;

       

        public async Task<DevGptChatMessage> CompletePrompt(IList<DevGptChatMessage> allMessages,
            IList<ICommandBase> commands = null)
        {
            //make sure the context messages are at the end of the list
            var contextMessages = allMessages.Where(m => m.Role == DevGptChatRole.ContextMessage).ToList();
            var otherMessages = allMessages.Where(m => m.Role != DevGptChatRole.ContextMessage).ToList();
            allMessages = otherMessages.Concat(contextMessages).ToList();


            var client = GetOpenAiClient();

            var messages = new List<Message>();
            foreach (var message in allMessages)
            {
                if (message is DevGptToolCallResultMessage toolMessage)
                {
                    //find toolcall bases on ID
                    var originalCall = messageLookup.Values.Where(c=>c.ToolCalls !=null).SelectMany(c => c.ToolCalls)
                        .FirstOrDefault(m => m.Id == toolMessage.ToolCallId);

                    var dotnetToolMessage = (IList<Message>)new[]
                    {
                        new Message( originalCall,message.Content.Select(DotnetChatMessageMapper.Map))
                    };
                    
                    messages.AddRange(dotnetToolMessage);
                }
                else
                {
                    //assistant reply message are replaced with original message. User messages are mapped
                    messages.Add(messageLookup.ContainsKey(message)
                        ? messageLookup[message]
                        : DotnetChatMessageMapper.Map(message));
                }
            }

            var model = "gpt-4o";
            var adapters = commands?.Select(c => new FunctionAdapter(c)).ToList();
            IEnumerable<Tool>? tools = adapters?.Select(a => (Tool)a.GetFunction()).ToList();

            var chatRequest = CreateChatRequest(tools, messages, model);

            Console.ForegroundColor = ConsoleColor.Blue;


            var completions = await client.GetCompletionAsync(chatRequest);

            var messageContent = completions.FirstChoice.Message.Content?.ToString();

            var devGptToolCalls = GetDevGptToolCalls(completions);

            Console.ForegroundColor = ConsoleColor.Blue;
            var inputCost = (completions.Usage.PromptTokens * 0.01 / 1000) ;
            var outputCost = (completions.Usage.CompletionTokens * 0.03 / 1000) ;
            totalRunCosts += inputCost + outputCost;
            Console.WriteLine($"** Usage {completions.Usage.TotalTokens} tokens **. Cost of prompt {inputCost+outputCost:C}");
            Console.WriteLine($"** Total costs {totalRunCosts:C}");

            var resultMessage = new DevGptChatMessage(DevGptChatRole.Assistant, messageContent, devGptToolCalls);
            
            messageLookup.Add(resultMessage, completions.FirstChoice.Message);
            return resultMessage;
        }

        private ChatRequest CreateChatRequest(IEnumerable<Tool>? tools, List<Message> messages, string model)
        {
            if (_disableFunctionCalling)
            {
                return new ChatRequest(messages
                    , model: model, temperature: 0.7, maxTokens: 1500);
            }

            return new ChatRequest(messages, tools: tools ?? Enumerable.Empty<Tool>(), toolChoice: "auto"
                , model:
                model, temperature: 0.7, maxTokens: 1500, responseFormat: ChatResponseFormat.Json);
        }

        private static IList<DevGptToolCall> GetDevGptToolCalls(ChatResponse completions)
        {
            var toolCalls = completions.FirstChoice.Message.ToolCalls;
            if (toolCalls == null || !toolCalls.Any())
            {
                return new List<DevGptToolCall>();
            }
            return toolCalls.Select(ConvertToDevGptToolCall).ToList();
        }

        private static DevGptToolCall ConvertToDevGptToolCall(Tool toolCall)
        {
            var functionName = toolCall?.Function?.Name;
            var arguments = toolCall?.Function.Arguments.AsValue().ToString();
            var jsonObject = JsonObject.Parse(arguments).AsObject();
            var argumentValues = jsonObject.Select(x => x.Value.AsValue().ToString()).ToList();

            return new DevGptToolCall(functionName, argumentValues, toolCall.Id);
        }

        private IDotnetOpenAiClientChatEndpoint GetOpenAiClient()
        {
            var useCache = false;

            ChatEndpoint innerChatEndpoint;
            if (_clientType == OpenAiClientType.AzureOpenAI)
            {
                var azureKey = Environment.GetEnvironmentVariable("DevGpt_AzureKey", EnvironmentVariableTarget.User);
                var azureOpenAIResource = Environment.GetEnvironmentVariable("DevGpt_AzureResource", EnvironmentVariableTarget.User);

                var auth = new OpenAIAuthentication(azureKey);
                var settings = new OpenAIClientSettings(resourceName: azureOpenAIResource, deploymentId: "gpt-4-1106-preview", apiVersion: "2023-07-01-preview");

                innerChatEndpoint = new OpenAIClient(auth, settings).ChatEndpoint;
            }
            else
            {
                var openAIKey = Environment.GetEnvironmentVariable("DevGpt_OpenAIKey", EnvironmentVariableTarget.User);
                innerChatEndpoint = new OpenAIClient(openAIKey).ChatEndpoint;
            }
            
            if (useCache)
            {
                return new RedisCachingDotnetOpenAiClient(innerChatEndpoint, new RedisClient());
            }

            return new NonCachingDotOpenAiClientWrapper(innerChatEndpoint);
        }
    }
}
