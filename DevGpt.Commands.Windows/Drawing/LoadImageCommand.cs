using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Commands.Windows.Drawing;
using DevGpt.Models.Commands;

namespace DevGpt.Commands.Windows.Drawing
{
    public interface IImageContext
    {
        void LoadImage(string path);
        void DrawRectangle(int x, int y, int width, int height);
        void SaveImage(string path);
        Image GetImage();
    }

    public class LoadImageCommand: ICommand
    {
        private readonly IImageContext _imageContext;

        public LoadImageCommand(IImageContext imageContext)
        {
            _imageContext = imageContext;
        }


        public string[] Arguments => new[] { "path" };
        public string Description => "Loads an image from a file";
        public string Name => "LoadImage";
        public string Execute(string[] args)
        {
            // Load png image from file
            if (args.Length != 1)
            {
                throw new ArgumentException("Invalid number of arguments");
            }
            _imageContext.LoadImage(args[0]);
            return "Image loaded";
        }
    }
}
