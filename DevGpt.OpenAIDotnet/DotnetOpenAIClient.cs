using System.ComponentModel.Design;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
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
        private IDictionary<DevGptChatMessage, Message> messageLookup = new Dictionary<DevGptChatMessage, Message>();

        public async Task<DevGptChatMessage> CompletePrompt(IList<DevGptChatMessage> allMessages,
            IList<ICommandBase> commands = null)
        {
            var client = GetOpenAiClient();
            double? temp = 0.5;
            // ### If streaming is selected

            var messages = new List<Message>();
            foreach (var message in allMessages)
            {
                if (message is DevGptToolCallResultMessage toolMessage)
                {
                    //find toolcall bases on ID
                    var originalCall = messageLookup.Values.SelectMany(c => c.ToolCalls)
                        .FirstOrDefault(m => m.Id == toolMessage.ToolCallId);

                    //var correspondingToolCall = toolMessage.ToolCallMessage.ToolCalls.First(t=>t.Id == toolMessage.ToolCallId);
                    var dotnetToolMessage = (IList<Message>)new[]
                    {
                        new Message( originalCall,toolMessage.Result)
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

            var model = allMessages.Any(m => m.Content.Any(c => c.ContentType == DevGptContentType.ImageUrl))
                ? "gpt-4-vision-preview"
                : "gpt-4-1106-preview";
            
            var adapters = commands?.Select(c => new FunctionAdapter(c)).ToList();
            IEnumerable<Tool>? tools = adapters?.Select(a => (Tool)a.GetFunction()).ToList();
            

            var chatRequest = new ChatRequest(messages,tools: tools ?? Enumerable.Empty<Tool>(),toolChoice:"auto"
                ,model:
                model, temperature:0.5,maxTokens:1500,responseFormat:ChatResponseFormat.Json);
            

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

        private static ChatEndpoint GetOpenAiClient()
        {
            var useAzure = false;

            var openAIKey = Environment.GetEnvironmentVariable("DevGpt_OpenAIKey", EnvironmentVariableTarget.User); 
            return new OpenAIClient(openAIKey).ChatEndpoint;
        }


       
    }
}
