using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using DevGpt.Models.OpenAI;

namespace DevGpt.OpenAI
{
    internal class AzureOpenAIChatMessageMapper
    {
        public static ChatMessage Map(DevGptChatMessage message)
        {
            return new ChatMessage(Map(message.Role),message.Content.First().Content);
        }

        public static ChatRole Map(DevGptChatRole message)
        {
            return message == DevGptChatRole.User ? ChatRole.User :
                   message == DevGptChatRole.Assistant ? ChatRole.Assistant : 
                ChatRole.System;

        }
    }
}
