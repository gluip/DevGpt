using System.Windows.Input;

using DevGpt.Models.Commands;
using Markdig;
using ICommand = DevGpt.Models.Commands.ICommand;

namespace DevGpt.Command.WritePdf
{
    public class WritePdfFromMarkDownCommand : ICommand
    {
        public string[] Arguments => new[] { "path" ,"markdown"};
        public string Description => "writes pdf from markdown";
        public string Name => "write_pdf_from_markdown";
        public string Execute(string[] args)
        {
            if (args.Length != 2)
            {
                return $"{Name} requires 2 arguments path and markdown";
            }   
            var path = args[0];
            var markdown = args[1];

            string html = Markdown.ToHtml(markdown);

            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html);
            doc.Save(path);
            doc.Close();

            return $"{Name} of '{path}' succeeded";
        }
    }
}
