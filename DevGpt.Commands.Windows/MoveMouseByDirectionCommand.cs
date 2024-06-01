using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;

namespace DevGpt.Commands.Windows;

public class MoveMouseByDirectionCommand : WindowsCommandBase, IAsyncMessageCommand
{
    private int _speed;
    public string[] Arguments => new[] { "direction (left,right,up,down)" };
    public string Description => "Moves the mouse in a certain direction";
    public string Name => "MoveMouseByDirection";

    public async Task<IList<DevGptChatMessage>> ExecuteAsync(string[] args)
    {
        if (args.Length != 1)
        {
            throw new ArgumentException("Invalid number of arguments");
        }

        var direction = args[0];
        var pos = System.Windows.Forms.Cursor.Position;
        _speed = 100;
        switch (direction)
        {
            case "up":
                Cursor.Position = new Point(pos.X, pos.Y - _speed);
                break;
            case "down":
                Cursor.Position = new Point(pos.X, pos.Y + _speed);
                break;
            case "left":
                Cursor.Position = new Point(pos.X - _speed, pos.Y);
                break;
            case "right":
                Cursor.Position = new Point(pos.X + _speed, pos.Y);
                break;
            default:
                throw new ArgumentException("Invalid direction");
        }

        var devGptToolCallResultMessage = new DevGptToolCallResultMessage(Name, "Mouse moved.");

        return ScreenshotMessage(devGptToolCallResultMessage,false);
    }


}