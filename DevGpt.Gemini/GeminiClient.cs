using System.Net;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using GenerativeAI.Models;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using GenerativeAI.Tools;
using GenerativeAI.Types;
using System.Text.Json.Nodes;

namespace DevGpt.Gemini
{
    public class GeminiClient : IDevGptOpenAIClient
    {
        private IDictionary<DevGptChatMessage, Content> messageLookup = new Dictionary<DevGptChatMessage, Content>();

        public async Task<DevGptChatMessage> CompletePrompt(IList<DevGptChatMessage> allMessages, IList<ICommandBase> commands = null)
        {
            var apiKey = System.Environment.GetEnvironmentVariable("DevGpt_GeminiKey");

            //make sure the context messages are at the front of the list

            // add the content of the context messages to the first user message

            var nonContextMessages = allMessages.Where(m => m.Role != DevGptChatRole.ContextMessage).ToList();
            //allMessages = allMessages.Where(m => m.Role != DevGptChatRole.ContextMessage).ToList();



            var model = new GenerativeModel(apiKey, client: new HttpClient(new HttpClientHandler
            {
                Proxy = new WebProxy("http://127.0.0.1:8888")
            }))
            {
                AutoCallFunction = false,FunctionEnabled = true
            };

            var contents = new List<Content>();
            foreach (var message in nonContextMessages)
            {
                if (message is DevGptToolCallResultMessage toolMessage)
                {
                    var jsonObject = new JsonObject
                    {
                        { "result", toolMessage.Content.First().Content }
                    };
                    contents.Add(new Content
                    {
                        Parts = new[]
                        {
                            new Part
                            {
                                FunctionResponse = new ChatFunctionResponse{
                                    Name = toolMessage.ToolName,
                                    Response = new FunctionResponse{
                                        Name = toolMessage.ToolName,
                                        Content = jsonObject
                                    }
                            }
                        },
                        },
                        Role = "function"

                    });
                }
                else
                {
                    contents.Add(messageLookup.ContainsKey(message)
                        ? messageLookup[message]
                        : MapToContent(message));
                }
                //assistant reply message are replaced with original message. User messages are mapped


            }


            var contextMessages = allMessages.Where(m => m.Role == DevGptChatRole.ContextMessage).ToList();
            // add the content of the context messages to the first user message
            if (contextMessages.Any())
            {
                foreach (var cm in contextMessages)
                {
                    contents[0].Parts.First().Text += Environment.NewLine + "CONTEXT:" + Environment.NewLine + cm.Content.First().Content;
                }
            }
            //var contents = allMessages.Select(MapToContent).ToArray();


            var functions = commands.Select(MapToTool).ToList();
            var contentResponse = await model.GenerateContentAsync(new GenerateContentRequest
            {
                Contents = contents.ToArray(),
                Tools = new List<GenerativeAITool>{ new GenerativeAITool
                {
                    FunctionDeclaration = functions
                }}
            });

            var calls = GetDevGptToolCalls(contentResponse);

            var firstContent = contentResponse.Candidates.First().Content;
            var messageContent = firstContent.Parts.First().Text;
            var devGptChatMessage = new DevGptChatMessage(DevGptChatRole.Assistant, messageContent, calls);

            messageLookup.Add(devGptChatMessage, firstContent);

            return devGptChatMessage;
        }

        private static IList<DevGptToolCall> GetDevGptToolCalls(EnhancedGenerateContentResponse completions)
        {
            var toolCalls = completions.Candidates.First().Content.Parts.Where(p => p.FunctionCall != null);
            if (toolCalls == null || !toolCalls.Any())
            {
                return new List<DevGptToolCall>();
            }
            return toolCalls.Select(p => ConvertToDevGptToolCall(p.FunctionCall)).ToList();
        }

        private static DevGptToolCall ConvertToDevGptToolCall(ChatFunctionCall? toolCall)
        {
            var functionName = toolCall.Name;
            var argumentsValues = toolCall.Arguments.Values.Select(o => o.ToString()).ToList();
            //var jsonObject = JsonObject.Parse(argumentsValues).AsObject();
            //var argumentValues = jsonObject.Select(x => x.Value.AsValue().ToString()).ToList();

            //return null;
            return new DevGptToolCall(functionName, argumentsValues, "");

        }

        private ChatCompletionFunction MapToTool(ICommandBase arg)
        {
            return
                new ChatCompletionFunction
                {
                    Name = arg.Name,
                    Parameters = new ChatCompletionFunctionParameters
                    {
                        AdditionalProperties = MapToPropertiesDictionary(arg),
                    },
                    Description = arg.Description

                };



        }

        private static Dictionary<string, object> MapToPropertiesDictionary(ICommandBase arg)
        {
            var result = new Dictionary<string, object>
            {
                { "type", "object" }
            };
            var properties = new Dictionary<string, object>();
            foreach (var argument in arg.Arguments)
            {
                properties[argument] = new
                {
                    type = "string",
                    description = $"The {argument} argument"
                };
            }


            result.Add("properties", properties);

            return result;
        }

        private DevGptChatMessage MapToMessage(EnhancedGenerateContentResponse contentResponse)
        {
            return new DevGptChatMessage(DevGptChatRole.Assistant, contentResponse.Candidates.First().Content.Parts.First().Text);
        }

        private static Content MapToContent(DevGptChatMessage message)
        {
            var parts = message.Content.Select(MapToPart).ToArray();
            return new Content
            {
                Parts = parts,
                Role = MapToRole(message.Role)
            };
        }

        private static string? MapToRole(DevGptChatRole messageRole)
        {
            switch (messageRole)
            {
                case DevGptChatRole.User:
                    return "user";
                case DevGptChatRole.System:
                    return "user";
                case DevGptChatRole.Assistant:
                    return "model";
                case DevGptChatRole.ContextMessage:
                    return "user";
                case DevGptChatRole.Tool:
                    return "user";
                default:
                    throw new ArgumentOutOfRangeException(nameof(messageRole), messageRole, null);
            }
        }

        private static Part MapToPart(DevGptContent arg)
        {
            return new Part
            {
                Text = arg.Content
            };
        }
    }
}
