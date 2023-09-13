using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands;

public class WriteFileCommand : ICommand
{
    public string Execute(params string[] args)
    {
        if (args.Length != 2)
        {
            return $"{Name} requires 2 arguments path and content";
        }

        try
        {
            var path = args[0];
            var content = args[1];
            File.WriteAllText(path, content);
            return $"{Name} of '{path}' succeeded";
        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }
    public string Name => "write_file";
    public string Description => "writes a file";
    public string[] Arguments => new[]{"path","content"};

}