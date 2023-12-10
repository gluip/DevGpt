
using DevGpt.Models.OpenAI;
using DevGpt.OpenAI;
using OpenAI;
using OpenAI.Chat;
using static System.Net.Mime.MediaTypeNames;

namespace DevGpt.Images // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static async Task Main(string[] args)
        {

            // See https://aka.ms/new-console-template for more information
            Console.WriteLine("Hello, World!");

            var openAIKey = Environment.GetEnvironmentVariable("DevGpt_OpenAIKey", EnvironmentVariableTarget.User);


            var client = new DotnetOpenAIClient(true);
            //read png from file into bas64 string
            var base64 = Convert.ToBase64String(File.ReadAllBytes("orv.png"));

            var uri = $"data:image/jpeg;base64,{base64}";

            var messages = new List<DevGptChatMessage>
            {
                new DevGptChatMessage(DevGptChatRole.System, "You are a helpful assistant."),
                new DevGptChatMessage(DevGptChatRole.User, new List<DevGptContent>
                {
                    new DevGptContent(DevGptContentType.Text, "Is the 'Mijzelf and iemand anders' option selected?"),
                    new DevGptContent(DevGptContentType.ImageUrl,uri)
                })
            };
            //var chatRequest = new ChatRequest(messages, model: "gpt-4-vision-preview",maxTokens:1000);
            var result = await client.CompletePrompt(messages);
            Console.WriteLine(result);
        }
    }
}
