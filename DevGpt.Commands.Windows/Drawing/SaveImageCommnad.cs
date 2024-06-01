using DevGpt.Models.Commands;

namespace DevGpt.Commands.Windows.Drawing;

public class SaveImageCommnad : ICommand
{
    private readonly IImageContext _imageContext;

    public SaveImageCommnad(IImageContext imageContext)
    {
        _imageContext = imageContext;
    }

    public string[] Arguments => new[] { "path" };
    public string Description => "Saves the image to a file";
    public string Name => "SaveImage";

    public string Execute(string[] args)
    {
        if (args.Length != 1)
        {
            throw new ArgumentException("Invalid number of arguments");
        }
        _imageContext.SaveImage(args[0]);
        return "Image saved";
    }
}