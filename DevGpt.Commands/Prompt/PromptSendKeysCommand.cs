using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Commands;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands
{
    public class PromptSendKeysCommand : ICommand
    {
        private readonly ICommandPrompt commandPrompt;

        public PromptSendKeysCommand(ICommandPrompt commandPrompt)
        {
            this.commandPrompt = commandPrompt;
        }
        public string[] Arguments => new string[] {"keys"};

        public string Description => "sends keys to the command prompt";
        public string Name => "prompt_send_keys";
        public string Execute(string[] args)
        {
            if (args.Length != 1)
            {
                return $"{Name} requires 1 argument: keys";
            }
            commandPrompt.SendKeys(args[0]);
            return "Keys sent. Output changed to:"+commandPrompt.ReadOutput();
        }
    }
}
