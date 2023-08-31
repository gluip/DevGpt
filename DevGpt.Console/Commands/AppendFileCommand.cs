using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands;

public class AppendFileCommand : ICommand
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
            File.AppendAllText(path, content);
            File.AppendAllText(path,Environment.NewLine);
            return $"{Name} of '{path}' succeeded";
        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }
    public string Name => "append_file";
    public string Description => "appends text to a file";
    public string[] Arguments => new[]{"path","content"};

}