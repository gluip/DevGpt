using DevGpt.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DevGpt.Models.OpenAI;
using Microsoft.Playwright;

namespace DevGpt.Commands.Web.Semantic
{
    public class ImageQuestionCommand : IAsyncMessageCommand
    {
        private readonly IDevGptOpenAIClient _devGptOpenAiClient;

        public ImageQuestionCommand(IDevGptOpenAIClient devGptOpenAiClient)
        {
            _devGptOpenAiClient = devGptOpenAiClient;
        }

        public string[] Arguments => new[] { "path", "question" };

        public string Description => "Answer a question about an image";
        public string Name => "image_question";
        public async Task<IList<DevGptChatMessage>> ExecuteAsync(string[] args)
        {
            var path = args[0];
            var question = args[1];

            //read the image to base64 string
            var image = System.IO.File.ReadAllBytes(path);
            var base64Image = Convert.ToBase64String(image);

            var devGptChatMessage = new DevGptChatMessage(DevGptChatRole.User, new List<DevGptContent>
            {
                new DevGptContent(DevGptContentType.Text, question),
                new DevGptContent(DevGptContentType.ImageUrl, $"data:image/jpeg;base64,{base64Image}")
            });

            var answer = await _devGptOpenAiClient.CompletePrompt(new[] { devGptChatMessage });

            return new[]
            {
                new DevGptChatMessage(DevGptChatRole.User, answer.Content.First().Content)
            };
        }
    }
}
