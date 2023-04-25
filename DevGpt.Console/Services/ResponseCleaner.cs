using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevGpt.Console.Services
{
    public class ResponseCleaner
    {
        public string CleanJsonResponse(string content)
        {
            // regex select text between quotes
            
            var regex = new Regex("\"([\\s\\S]*?)\"",RegexOptions.Multiline);
            var matches = regex.Matches(content);
            foreach (Match match in matches)
            {
                var value = match.Value;
                if (value.Contains("\r\n") )
                {
                    var newValue = value.Replace("\r\n", "\\r\\n");
                    content = content.Replace(value, newValue);
                }
            }

            return content;
        }
    }
}
