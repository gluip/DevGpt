public interface ICommand
{
    string[] Arguments { get; }
    string Description { get; }
    string Name { get; }
    string Execute(string[] args);
}

public static class CommandExtensions
{
    public static string GetHelp(this ICommand command)
    {
        var arguments = string.Join(",",command.Arguments.Select(arg=>$"\"{arg}\""));
        return $"\"{command.Name}\" args: \"{arguments}\" - {command.Description}";
    }
}