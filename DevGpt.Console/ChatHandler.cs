
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Console.Logging;

using DevGpt.Models.OpenAI;

namespace DevGpt.Console
{
    internal class ChatHandler
    {
        private  IList<DevGptChatMessage> Messages { get; set; } = new List<DevGptChatMessage>();

        public IList<DevGptChatMessage> GetMessages()
        {
            return Messages;
        }
        //    //remove all context messages from the list
        //    Messages = Messages.Where(m => !m.IsContext).ToList();
        //    return result;

        //}

        public void AddMessage(DevGptChatMessage message)
        {
            //_memoryManager.StoreMessage(message.Content);
            Logger.LogMessage(message.Role +":" + message.Content);
            Messages.Add(message);

            //assistant messages are logged elsewhere
            if (message.Role == DevGptChatRole.Assistant)
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
            System.Console.ForegroundColor = message.Role == DevGptChatRole.User ? 
                ConsoleColor.Green : message.Role == DevGptChatRole.Assistant ?
                ConsoleColor.Red:
                ConsoleColor.White;
            System.Console.WriteLine(message.Role + ":" + message.ToString());
        }

    }
}
