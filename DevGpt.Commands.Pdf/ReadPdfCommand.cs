using System.Text;
using DevGpt.Models.Commands;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;

namespace DevGpt.Commands.Pdf
{
    public class ReadPdfCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                return "Invalid number of arguments. Read pdf requires one argument : path";
            }

            if (!args[0].EndsWith(".pdf"))
            {
                return "Invalid file type only pdf files are supported.";
            }

            using PdfDocument document = PdfDocument.Open(args[0]);
            
            foreach (Page page in document.GetPages())
            {
                IReadOnlyList<Letter> letters = page.Letters;
                return string.Join(string.Empty, letters.Select(x => x.Value));
            }

            return "";

        }
        public string Name => "read_pdf-file";
        public string Description => "reads a pdf file";

        public string[] Arguments => new[] { "path" };
    }
}