using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Commands;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands
{
    public class PromptSendLeftArrowKeyCommand : ICommand
    {
        private readonly ICommandPrompt commandPrompt;

        public PromptSendLeftArrowKeyCommand(ICommandPrompt commandPrompt)
        {
            this.commandPrompt = commandPrompt;
        }
        public string[] Arguments => new string[] {""};

        public string Description => "sends a left arrow key to the command prompt";
        public string Name => "prompt_send_left_arrow";
        public string Execute(string[] args)
        {
            commandPrompt.SendLeftArrowKey();
            return "Left arrow key sent. Output changed to:"+commandPrompt.ReadOutput();
        }
    }
}
