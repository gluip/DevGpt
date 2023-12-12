using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models.Commands;

namespace DevGpt.Commands.Commands
{
    public class GetCurrentDateCommand:ICommand
    {
        public string Name => "get_current_date";
        public string Execute(string[] args)
        {
            return DateTime.Today.ToString("D");

        }

        public string[] Arguments => Array.Empty<string>();
        public string Description => "Gets the current date";

      
    }
}
