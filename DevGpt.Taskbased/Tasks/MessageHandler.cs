using Azure.AI.OpenAI;
using DevGpt.Models.Utils;

namespace DevGpt.Console.Tasks;

class MessageHandler : IMessageHandler
{
    public void HandleMessage(ChatRole chatRole, string message)
    {
        var color = chatRole == ChatRole.User ? 
            ConsoleColor.Green : ConsoleColor.Red;

        DevConsole.WriteLine(message, color);

        var path = $@"c:\devgpt\logs\{chatRole.ToString()}_log{DateTime.Now:hh_mm_ss}.txt";
        System.IO.File.AppendAllText(path, message);

        
    }
}