
using DevGpt.Models.OpenAI;
using DevGpt.Models.Utils;

namespace DevGpt.Console.Tasks;

class MessageHandler : IMessageHandler
{
    public void HandleMessage(DevGptChatRole chatRole, string message)
    {
        var color = chatRole == DevGptChatRole.User ? 
            ConsoleColor.Green : ConsoleColor.Red;

        DevConsole.WriteLine(message, color);

        var path = $@"c:\devgpt\logs\{chatRole.ToString()}_log{DateTime.Now:hh_mm_ss}.txt";
        System.IO.File.AppendAllText(path, message);

        
    }
}