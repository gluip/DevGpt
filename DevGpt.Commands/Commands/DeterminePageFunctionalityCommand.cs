using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands;

public class DeterminePageFunctionalityCommand : ICommand
{
    public string Execute(params string[] args)
    {
        if (args.Length != 1)
        {
            return $"{Name} requires 1 arguments html";
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
    public string Name => "determine_page_functionality";
    public string Description => "inspects the page html and determines functionality";
    public string[] Arguments => new[]{"path","content"};

}