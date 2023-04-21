using DevGpt.Console.Commands;

namespace DevGpt.Test.Commands;

public class WriteFileCommandTest
{
    private WriteFileCommand command;

    public WriteFileCommandTest()
    {
        command = new WriteFileCommand();
    }

    [Fact]
    public void Execute_InvalidNumberOfArguments()
    {
        var result = command.Execute("some_file.txt");
        Assert.Equal("write_file requires 2 arguments path and content", result);
    }

    [Fact]
    public void Execute_ValidFile_ReturnsContentAndFilename()
    {
        var result = command.Execute("sample.txt", "some content");
        Assert.Equal("write_file of 'sample.txt' succeeded", result);
    }
}
