namespace DevGpt.Commands.Pdf.Test
{
    public class ReadPdfCommandTest
    {
        [Fact]
        public void ExecuteWithCorrectPdf_ExtractsText()
        {
            var command = new ReadPdfCommand();
            var result = command.Execute(@"Sample/Factuur.pdf");
            Assert.Contains("Muurman ICT", result);

        }
    }
}