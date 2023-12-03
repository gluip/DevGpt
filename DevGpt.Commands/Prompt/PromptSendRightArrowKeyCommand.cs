using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Commands;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands
{
    public class PromptSendRightArrowKeyCommand : ICommand
    {
        private readonly ICommandPrompt commandPrompt;

        public PromptSendRightArrowKeyCommand(ICommandPrompt commandPrompt)
        {
            this.commandPrompt = commandPrompt;
        }
        public string[] Arguments => new string[] {""};

        public string Description => "sends a right arrow key to the command prompt";
        public string Name => "prompt_send_right_arrow";
        public string Execute(string[] args)
        {
          
            commandPrompt.SendRightArrowKey();
            return "Right arrow key sent. Output changed to:"+commandPrompt.ReadOutput();
        }
    }
}
