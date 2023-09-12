using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Console.Logging
{
    internal class Logger
    {
        private static string promptname;
        public static void LogMessage(string message)
        {
            //write reply to a file in c:\devgpt\logs with a timestamp
            var path = $@"c:\devgpt\logs\{promptname}log{DateTime.Now:hh_mm_ss}.txt";
            System.IO.File.AppendAllText(path, message);
        }

        public static void ConfigurePrompName(string name)
        {
            promptname = name;
        }
    }
}
