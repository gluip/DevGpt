using DevGpt.Commands.Commands;
using DevGpt.Console.Commands;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;

namespace DevGpt.Gemini.Test
{
    public class GeminiClientTest
    {
        [Fact]
        public async Task Client_CanSend_SimpleMessage()
        {
            var client = new GeminiClient();
            var response =await client.CompletePrompt(new List<DevGptChatMessage>
            {
                new DevGptChatMessage(DevGptChatRole.User, "Tell me a poem")
            });

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Client_CanUseFunctionCalls()
        {
            var client = new GeminiClient();
            var response = await client.CompletePrompt(new List<DevGptChatMessage>
            {
                new DevGptChatMessage(DevGptChatRole.User, "What age am i today? I was born on 12 december 1979.")
            },new List<ICommandBase>{new GetCurrentDateCommand()});

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Client_CanUseFunctionCallsWithArguments()
        {
            var client = new GeminiClient();
            var response = await client.CompletePrompt(new List<DevGptChatMessage>
            {
                new DevGptChatMessage(DevGptChatRole.User, "What age am i today? I was born on 12 december 1979.")
            }, new List<ICommandBase> { new ReadFileCommand() });

            Assert.NotNull(response);
        }
    }
}