


using DevGpt.Models.OpenAI;

namespace DevGpt.Forms
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
            //Logger.LogMessage(message.ToString());
            
            if (message is DevGptContextMessage contextMessage)
            {
                //remove all context messages with the same key
                Messages = Messages.Where(m => !(m is DevGptContextMessage cm && cm.ContextKey == contextMessage.ContextKey)).ToList();
            }

            Messages.Add(message);

            //assistant messages are logged elsewhere
            if (message.Role == DevGptChatRole.Assistant)
            {
                return;
            }
            
            //write to console color based on role
            System.Console.ForegroundColor = message.Role == DevGptChatRole.User ? 
                ConsoleColor.Green : message.Role == DevGptChatRole.Assistant ?
                ConsoleColor.Red:
                ConsoleColor.White;
            System.Console.WriteLine(message.ToString());
        }

        public void RemoveContextMessages()
        {
            Messages = Messages.Where(m => !m.IsContext).ToList();
            
        }
    }
}
