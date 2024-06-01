using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;

namespace DevGpt.Commands.Windows;

public class GetScreenShotCommand : WindowsCommandBase, IAsyncMessageCommand
{
    public string[] Arguments => new string[0];
    public string Description => "Get a screenshot of the screen";
    public string Name => "GetScreenShot";
    public async Task<IList<DevGptChatMessage>> ExecuteAsync(string[] args)
    {
        if (args.Length != 0)
        {
            throw new ArgumentException("Invalid number of arguments");
        }
        var devGptToolCallResultMessage = new DevGptToolCallResultMessage(Name, "image updated in context");
        return ScreenshotMessage(devGptToolCallResultMessage,true);
    }
}