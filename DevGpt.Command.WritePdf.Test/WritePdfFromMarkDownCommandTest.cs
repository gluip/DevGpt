namespace DevGpt.Command.WritePdf.Test
{
    public class WritePdfFromMarkDownCommandTest
    {
        [Fact]
        public void Execute_WithProperMarkDown_WritesPdf()
        {
            var command = new WritePdfFromMarkDownCommand();
            var path = Path.GetTempFileName();
            var markdown = "# Hello World";
            var result = command.Execute(new[] { path, markdown });
            Assert.Equal($"write_pdf_from_markdown of '{path}' succeeded", result);
        }
    }
}