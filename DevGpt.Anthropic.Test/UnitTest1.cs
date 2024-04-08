using Anthropic.SDK.Constants;
using Anthropic.SDK.Messaging;
using Anthropic.SDK;

namespace DevGpt.Anthropic.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var client = new AnthropicClient();
            var messages = new List<Message>();
            messages.Add(new Message()
            {
                Role = RoleType.User,
                Content = "Write me a sonnet about the Statue of Liberty"
            });
            var parameters = new MessageParameters()
            {
                Messages = messages,
                MaxTokens = 512,
                Model = AnthropicModels.Claude3Opus,
                Stream = false,
                Temperature = 1.0m,
            };
            var res = await client.Messages.GetClaudeMessageAsync(parameters);

        }
    }
}