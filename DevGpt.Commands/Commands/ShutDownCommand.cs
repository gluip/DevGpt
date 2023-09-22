using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands;

public class ShutDownCommand : ICommand
{
    public string Execute(params string[] args)
    {
        Environment.Exit(0);
        return "shutting down";
    }
    public string Name => "shutdown";
    public string Description => "shuts down the application";
    public string[] Arguments => new string[0];
}