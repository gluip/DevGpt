namespace DevGpt.Models.Commands;

public static class CommandExtensions
{
    public static string GetHelp(this ICommandBase command)
    {
        var arguments = string.Join(",",command.Arguments.Select(arg=>$"\"{arg}\""));
        return $"\"{command.Name}\" args: \"{arguments}\" - {command.Description}";
    }
}