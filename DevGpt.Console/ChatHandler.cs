using Azure.AI.OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Console
{
    internal class ChatHandler
    {
        public IList<ChatMessage> Messages { get; } = new List<ChatMessage>();

        public void AddMessage(ChatMessage message)
        {
            Messages.Add(message);
            //write to console color based on role
            System.Console.ForegroundColor = message.Role == ChatRole.User ? 
                ConsoleColor.Green : message.Role == ChatRole.Assistant ?
                ConsoleColor.Red:
                ConsoleColor.White;
            System.Console.WriteLine(message.Role + ":" + message.Content);
        }

    }
}
