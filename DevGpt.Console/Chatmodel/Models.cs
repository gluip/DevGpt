using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Console.Chatmodel
{
    internal class Models
    {
        public class Command
        {
            public string name { get; set; }
            public List<string> args { get; set; }
        }

        public class AssitantReply
        {
            public Thoughts thoughts { get; set; }
            public Command command { get; set; }
        }

        public class Thoughts
        {
            public string text { get; set; }
            public string reasoning { get; set; }
            public string plan { get; set; }
            public string criticism { get; set; }
            public string speak { get; set; }
        }


    }
}
