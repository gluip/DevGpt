using DevGpt.Models.Commands;

namespace DevGpt.Models.Commands;



public interface ICommand : ICommandBase
{ 
    string Execute(string[] args);

}