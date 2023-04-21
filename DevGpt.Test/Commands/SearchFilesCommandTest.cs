using DevGpt.Console.Commands;

namespace DevGpt.Test.Commands;

public class SearchFilesCommandTest
{
    private SearchFilesCommand command;

    public SearchFilesCommandTest()
    {
        command = new SearchFilesCommand();
    }

    [Fact]
    public void Execute_InvalidNumberOfArguments()
    {
        var result = command.Execute("some_file.txt");
        Assert.Equal("search_files requires 2 arguments path and searchPattern", result);
    }

    [Fact]
    public void Execute_ValidFile_ReturnsContentAndFilename()
    {
        var result = command.Execute(".", "*.txt");
        Assert.Contains("sample.txt", result);
    }
}