using System.Net;
using Anthropic.SDK;
using Anthropic.SDK.Constants;
using Anthropic.SDK.Messaging;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;

namespace DevGpt.Anthropic
{
    public class FiddlerHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            return new HttpClient(new HttpClientHandler()
            {
                Proxy = new WebProxy("http://127.0.0.1:8888"),
            });
        }
    }
    public class AnthropicDevGptClient : IDevGptOpenAIClient
    {
        private readonly AnthropicClient _client;

        public AnthropicDevGptClient()
        {
            _client = new AnthropicClient();
            _client.HttpClientFactory = new FiddlerHttpClientFactory();
        }

        public async Task<DevGptChatMessage> CompletePrompt(IList<DevGptChatMessage> allMessages, IList<ICommandBase> commands = null)
        {
            var messages = new List<Message>();
            var tools = new List<Tool>();
            //var systemMessage = allMessages.First(m => m.Role == DevGptChatRole.System).Content[0].Content;

            messages = allMessages.Select(ClaudeMessageMapper.Map).ToList();
            tools = commands.Select(ClaudeMessageMapper.Map).ToList();
            var res = await _client.Messages.GetClaudeMessageAsync(new MessageParameters
            {
                Messages = messages,
                Tools = tools,
                MaxTokens = 512,
               // SystemMessage = systemMessage,
                Model = AnthropicModels.Claude3Opus,
                Stream = false,
                Temperature = 1.0m,
            });

            var devGptToolCalls = res.Content.Where(c=>c.Type == ContentType.tool_use).Select(MapToDevGptToolCall).ToList();
            var messageContent = res.Content.FirstOrDefault(c=>c.Type == ContentType.text)?.Text;
            return new DevGptChatMessage(DevGptChatRole.Assistant, messageContent,devGptToolCalls);

        }

        private DevGptToolCall MapToDevGptToolCall(ContentRespone arg)
        {
            return new DevGptToolCall(arg.Name, arg.Input.Values.ToList(), arg.Id);
        }
    }

    public class ClaudeMessageMapper
    {
        public static Message Map(DevGptChatMessage message)
        {
            var role = Map(message.Role);

            if (message is DevGptToolCallResultMessage resultMessage)
            {
                var toolResultContents = message.Content.Select(c =>
                {
                    var content = message.Content[0].Content;
                    return new ToolResultContent
                    {
                        ToolUseId = resultMessage.ToolCallId, 
                        Content = content
                    };
                });
                return new Message
                {
                    Content = toolResultContents,
                    Role = RoleType.User
                };
            }

            if (message.ToolCalls.Any())
            {
                //{
                //    "type": "tool_use",
                //    "id": "toolu_01A09q90qw90lq917835lq9", 
                //    "name": "get_weather",
                //    "input": { "location": "San Francisco, CA", "unit": "celsius"}
                //}
                return new Message
                {
                    Content = new dynamic[]
                    {
                        new TextContent
                        {
                            Text = message.Content[0].Content,
                        },
                        message.ToolCalls.Select(c => new ToolUseContent
                        {
                            Id = c.ToolCallId,
                            Name = c.ToolName,
                            //TODO figure out
                            Input = new Dictionary<string, string>()//c.Arguments
                        }).First()
                    } ,
                    Role = RoleType.Assistant
                };
            }
            else
            {
                return new Message
                {
                    Content = message.Content.Select(c => new
                    {
                        type = "text",
                        text = c.Content
                    }),
                    Role = role
                };
            }
            

        }

        public static Tool Map(ICommandBase command)
        {
            var tool = new Tool
            {
                Name = command.Name,
                Arguments = new InputSchema(),
                Description = command.Description,
            };

            if (command.Arguments.Any())
            {
                tool.Arguments.required = command.Arguments;
                tool.Arguments.properties = command.Arguments.ToDictionary(a => a.Replace(" ","_").Replace(".","_"), a => new Property
                {
                    type = "string", description = a
                });
            }

            return tool;

        }

        private static dynamic Map(DevGptContent messageContent)
        {
            return messageContent.Content;

        }

        private static string Map(DevGptChatRole messageRole)
        {
            return messageRole switch
            {
                DevGptChatRole.Assistant => RoleType.Assistant,
                DevGptChatRole.User => RoleType.User,
                DevGptChatRole.System => RoleType.User,
                DevGptChatRole.Tool => RoleType.User,
                _ => throw new ArgumentOutOfRangeException(nameof(messageRole), messageRole, null)
            };
        }
    }
}
