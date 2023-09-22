using Azure.AI.OpenAI;

namespace DevGpt.Console.Tasks;

class MessageHandler : IMessageHandler
{
    public void HandleMessage(ChatRole chatRole, string message)
    {
        System.Console.ForegroundColor = chatRole == ChatRole.User ? 
            ConsoleColor.Green : ConsoleColor.Red;
        System.Console.WriteLine(message);

        var path = $@"c:\devgpt\logs\{chatRole.ToString()}_log{DateTime.Now:hh_mm_ss}.txt";
        System.IO.File.AppendAllText(path, message);

        
    }
}