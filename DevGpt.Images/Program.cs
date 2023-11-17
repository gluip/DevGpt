
using OpenAI;
using OpenAI.Chat;

namespace DevGpt.Images // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static async Task Main(string[] args)
        {

            // See https://aka.ms/new-console-template for more information
            Console.WriteLine("Hello, World!");

            var openAIKey = Environment.GetEnvironmentVariable("DevGpt_OpenAIKey", EnvironmentVariableTarget.User);

            var client = new OpenAIClient(openAIKey);
            
            var messages = new List<Message>
            {
                new Message(Role.System, "You are a helpful assistant."),
                new Message(Role.User, new List<Content>
                {
                    new Content(ContentType.Text, "What's in this image?"),
                    new Content(ContentType.ImageUrl, "https://i.ibb.co/b12F8qQ/Screenshot-2023-11-16-230916.png")
                })
            };
            var chatRequest = new ChatRequest(messages, model: "gpt-4-vision-preview",maxTokens:1000);
            var result = await client.ChatEndpoint.GetCompletionAsync(chatRequest);
            Console.WriteLine($"{result.FirstChoice.Message.Role}: {result.FirstChoice.Message.Content} | Finish Reason: {result.FirstChoice.FinishDetails}");
        }
    }
}
