using Azure.AI.OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Memory;

namespace DevGpt.Console
{
    internal class ChatHandler
    {
        private readonly IMemoryManager _memoryManager;
        public ChatHandler(IMemoryManager memoryManager)
        {
            _memoryManager = memoryManager;
        }
        public IList<ChatMessage> Messages { get; } = new List<ChatMessage>();

        public void AddMessage(ChatMessage message)
        {
            _memoryManager.StoreMessage(message.Content);

            Messages.Add(message);

            //assistant messages are logged elsewhere
            if (message.Role == ChatRole.Assistant)
            {
                return;
            }

            //if (message.Role == ChatRole.User)
            //{
            //    var relevantMessages = _memoryManager.RetrieveRelevantMessages(message.Content, 3);
            //    foreach (var relevantMessage in relevantMessages)
            //    {
            //        System.Console.ForegroundColor = ConsoleColor.Yellow;
            //        System.Console.WriteLine($"Memory: {relevantMessage}");
            //    }
            //}

            //write to console color based on role
            System.Console.ForegroundColor = message.Role == ChatRole.User ? 
                ConsoleColor.Green : message.Role == ChatRole.Assistant ?
                ConsoleColor.Red:
                ConsoleColor.White;
            System.Console.WriteLine(message.Role + ":" + message.Content);
        }

    }
}
