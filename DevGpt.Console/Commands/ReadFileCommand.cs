using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Console.Commands
{
    public class ReadFileCommand : ICommand
    {
        public string Execute(string path)
        {
            try
            {
                return $"{Name} of '{path}' returned '{File.ReadAllText(path)}'";

            }
            catch (Exception ex)
            {
                return $"{Name} failed with the following error: {ex.Message}";
            }
        }

        public string Name => "read_file";
        public string Description => "reads a file";

        public string Arguments => "path";

    }
}
