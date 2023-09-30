using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Models.Utils
{
    public static class DevConsole
    {
        public static void WriteLine( string text, System.ConsoleColor color = ConsoleColor.White)
        {
            var oldColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = color;
            System.Console.WriteLine(text);
            System.Console.ForegroundColor = oldColor;
        }
    }
}
