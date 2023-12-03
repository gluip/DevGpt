using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Commands;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands
{
    public class PromptReadOutputCommand : ICommand
    {
        private readonly ICommandPrompt commandPrompt;

        public PromptReadOutputCommand(ICommandPrompt commandPrompt)
        {
            this.commandPrompt = commandPrompt;
        }
        public string[] Arguments => new string[] {};

        public string Description => "read the command prompt output";
        public string Name => "prompt_read_output";
        public string Execute(string[] args)
        {
            return commandPrompt.ReadOutput();
        }
    }
}
