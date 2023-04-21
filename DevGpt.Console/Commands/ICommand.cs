public interface ICommand
{
    string Arguments { get; }
    string Description { get; }
    string Name { get; }
    string Execute(string path);
}

public static class CommandExtensions
{
    public static string GetHelp(this ICommand command)
    {
        return $"\"{command.Name}\" args: \"{command.Arguments}\" - {command.Description}";
    }
}