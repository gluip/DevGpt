namespace DevGpt.Models.Commands;

public interface ICommandBase
{
    string[] Arguments { get; }
    string Description { get; }
    string Name { get; }

}