using DevGpt.Models.Commands;

namespace DevGpt.Commands.Windows;

public class MoveMouseCommand:ICommand
{
    public string[] Arguments => new[] { "x", "y" };
    public string Description => "Moves the mouse by x,y pixels";
    public string Name => "MoveMouse";
    public string Execute(string[] args)
    {
        if (args.Length != 2)
        {
            throw new ArgumentException("Invalid number of arguments");
        }

        var x = Convert.ToInt32(args[0]);
        var y = Convert.ToInt32(args[1]);
        var pos = System.Windows.Forms.Cursor.Position;
        Cursor.Position = new Point(pos.X + x, pos.Y + y);
        return $"Mouse moved to {pos.X + x}, {pos.Y + y}";
    }
}