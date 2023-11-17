using DevGpt.Commands.Magic;
using DevGpt.Commands.Web.Browser;
using DevGpt.Commands.Web.Google;
using DevGpt.Commands.Web.Semantic;
using DevGpt.Console.Commands.Semantic;


namespace DevGpt.Commands.Web.Test
{
    public class GoogleSearchCommandTest
    {
        [Fact]
        public async Task GoogleSearchCommand_GetRelevantSearchResult()
        {
            var command = new GoogleSearchCommand();
            var result = await command.ExecuteAsync(new []{"Erwin olaf" });
            Assert.Contains("Erwin olaf", result);
        }
    }
}