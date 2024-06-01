namespace DevGpt.Commands.Windows.Drawing;

public class ImageContext : IImageContext
{
    private Image image;

    public void LoadImage(string path)
    {
        // Load png image from file
        image = Image.FromFile(path);
    }

    public void DrawRectangle(int x, int y, int width, int height)
    {
        // Draw rectangle on image
        using (var graphics = Graphics.FromImage(image))
        {
            graphics.DrawRectangle(Pens.Black, x, y, width, height);
        }
    }

    public void SaveImage(string path)
    {
        // Save image to file
        image.Save(path);
    }

    public Image GetImage()
    {
        return image;
    }
}