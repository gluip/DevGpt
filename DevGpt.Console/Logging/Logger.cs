using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Console.Logging
{
    internal class Logger
    {
        public static void LogReply(string message)
        {
            //write reply to a file in c:\devgpt\logs with a timestamp
            var path = $@"c:\devgpt\logs\log{DateTime.Now:hh_mm_ss}.txt";
            System.IO.File.AppendAllText(path, message);
        }
    }
}
