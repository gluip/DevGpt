namespace DevGpt.Models.Commands;

public static class CommandExtensions
{
    public static string GetHelp(this ICommandBase command)
    {
        var arguments = string.Join(",",command.Arguments.Select(arg=>$"\"{arg}\""));
        return $"\"{command.Name}\" args: \"{arguments}\" - {command.Description}";
    }

    public static string GetCommandsText(this IList<ICommandBase> commands)
    {
        var commandsText = string.Join("\n", commands.Select(c => c.GetHelp()));
        commandsText += "\n\n";
        return commandsText;
    }
}