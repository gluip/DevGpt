using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Commands;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands
{
    public class PromptSendLineCommand : ICommand
    {
        private readonly ICommandPrompt _commandPrompt;

        public PromptSendLineCommand(ICommandPrompt commandPrompt)
        {
            this._commandPrompt = commandPrompt;
        }
        public string[] Arguments => new string[] {"line"};

        public string Description => "sends a line to the command prompt";
        public string Name => "prompt_send_line";
        public string Execute(string[] args)
        {
            if (args.Length != 1)
            {
                return $"{Name} requires 1 argument: keys";
            }
            _commandPrompt.SendLine(args[0]);
            return "Line sent. Output changed to:"+_commandPrompt.ReadOutput();
            return "Line sent. Output changed to:" + _commandPrompt.ReadOutput();
        }
    }
}
