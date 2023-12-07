using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Commands;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands
{
    public class PromptOpenCommand : ICommand
    {
        private readonly ICommandPrompt commandPrompt;

        public PromptOpenCommand(ICommandPrompt commandPrompt)
        {
            this.commandPrompt = commandPrompt;
        }
        public string[] Arguments => new string[] { };

        public string Description => "opens the command prompt";
        public string Name => "prompt_open";
        public string Execute(string[] args)
        {
            commandPrompt.Open();
            return "command prompt opened";
        }
    }
}
