using DevGpt.Models.Commands;

namespace DevGpt.Models.Commands;

public class ComplexResult
{
    public string Context { get; set; }
    public string Result { get; set; }
}
public interface IComplexCommand : ICommandBase
{
    Task<ComplexResult> ExecuteAsync(params string[] args);


}

public interface ICommand : ICommandBase
{ 
    string Execute(string[] args);

}