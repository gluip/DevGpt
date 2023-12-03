using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Commands;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands
{
    public class PromptSendEnterKeyCommand : ICommand
    {
        private readonly ICommandPrompt commandPrompt;

        public PromptSendEnterKeyCommand(ICommandPrompt commandPrompt)
        {
            this.commandPrompt = commandPrompt;
        }
        public string[] Arguments => new string[] {""};

        public string Description => "sends a enter key to the command prompt";
        public string Name => "prompt_send_enter";
        public string Execute(string[] args)
        {
            commandPrompt.SendEnterKey();
            return "Enter sent. Output changed to:"+commandPrompt.ReadOutput();
        }
    }
}
