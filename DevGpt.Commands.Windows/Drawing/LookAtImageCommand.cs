using DevGpt.Models.Commands;
using System.Drawing.Imaging;
using DevGpt.Models.OpenAI;

namespace DevGpt.Commands.Windows.Drawing;

public class LookAtImageCommand : IAsyncMessageCommand
{
    private readonly IImageContext _imageContext;

    public LookAtImageCommand(IImageContext imageContext)
    {
        _imageContext = imageContext;
    }

    public string[] Arguments => new string[0];
    public string Description => "Looks at the current image";
    public string Name => "LookAtImageCommand";

    public async Task<IList<DevGptChatMessage>> ExecuteAsync(string[] args)
    {
        if (args.Length != 0)
        {
            throw new ArgumentException("Invalid number of arguments");
        }
        var image = _imageContext.GetImage();

        //convert the image to base64
        using (var ms = new MemoryStream())
        {
            image.Save(ms, ImageFormat.Png);

            
            var data = Convert.ToBase64String(ms.ToArray());
            //convert to base64 Uri
            var dataImagePngBase64 = "data:image/png;base64," + data;
            //add a timestamp to screenshot
            return new List<DevGptChatMessage>(new []{new DevGptToolCallResultMessage(Name,new List<DevGptContent>()
            {
                new(DevGptContentType.ImageUrl, dataImagePngBase64)
            })}) ;
        }


        

    }
}