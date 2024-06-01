using DevGpt.Models.Commands;

namespace DevGpt.Commands.Windows.Drawing;

public class DrawRectangleCommand : ICommand
{
    private readonly IImageContext _imageContext;

    public DrawRectangleCommand(IImageContext imageContext)
    {
        _imageContext = imageContext;
    }

    public string[] Arguments => new[] { "x", "y", "width", "height" };
    public string Description => "Draws a rectangle on the image";
    public string Name => "DrawRectangle";

    public string Execute(string[] args)
    {
        if (args.Length != 4)
        {
            throw new ArgumentException("Invalid number of arguments");
        }
        var x = int.Parse(args[0]);
        var y = int.Parse(args[1]);
        var width = int.Parse(args[2]);
        var height = int.Parse(args[3]);
        _imageContext.DrawRectangle(x, y, width, height);
        return "Rectangle drawn";
    }
}