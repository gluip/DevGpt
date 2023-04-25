using DevGpt.Console.Commands;

namespace MyApp;

internal class CommandExecutor
{
    public static string Execute(string commandName, string[] args)
    {
        switch (commandName)
        {
            case "read_file":
                return new ReadFileCommand().Execute(args);
            case "write_file":
                return new WriteFileCommand().Execute(args);
            case "search_files":
                return new SearchFilesCommand().Execute(args);
            default:
                return $"Command {commandName} not found. ";
        }
    }
}