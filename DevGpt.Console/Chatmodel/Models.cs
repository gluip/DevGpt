using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevGpt.Console.Chatmodel
{
        public class UserReply
        {
            
            public string result { get; set; }

            public string resultsummary { get; set; }

            public string GetContent()
            {
                return result;
            }
        }
        public class Command
        {
            public string name { get; set; }
            public List<string> args { get; set; }
        }

        public class AssitantReply
        {
            public Thoughts thoughts { get; set; }
            public string GetContent()
            {
                return JsonSerializer.Serialize(this);
            }
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
