using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands
{
    public class DotnetAddReferenceCommand : ICommand
    {
        public string Name => "dotnet add reference";
        public string Description => "Adds a reference to a project. dotnet add [<PROJECT>] reference <REFERENCED_PROJECT_PATH>.";

        public string Execute(params string[] args)
        {
            if ( args.Length != 2)
            {
                return "Invalid number of arguments. Please specify project and referenced_project";
            }
            // start in a proces dotnet add ./PrimeService.Tests/PrimeService.Tests.csproj reference ./PrimeService/PrimeService.csproj

            return new ExecuteShellCommand().Execute("dotnet", "add ./PrimeService.Tests/PrimeService.Tests.csproj reference ./PrimeService/PrimeService.csproj");

        }

        public string[] Arguments => new[] { "path_to_project.csproj","path_to_referenced_project" };
    }

}
