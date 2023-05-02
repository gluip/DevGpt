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

            var regex = new Regex("\"([\\s\\S]*?)\"", RegexOptions.Multiline);
            var matches = regex.Matches(content);
            foreach (Match match in matches)
            {
                var value = match.Value;
                if (value.Contains("\r\n"))
                {
                    var newValue = value.Replace("\r\n", "\\r\\n");
                    content = content.Replace(value, newValue);
                }
            }

            return content;
        }

        public string BalanceBraces(string content)
        {
            var jsonWithoutData = Regex.Replace(content,"\"([\\s\\S]*?)\"", "\"\"");
            var leftBraces = jsonWithoutData.Count(x => x == '{');
            var rightBraces = jsonWithoutData.Count(x => x == '}');
            var difference = leftBraces - rightBraces;
            if (difference > 0)
            {
                for (int i = 0; i < difference; i++)
                {
                    content += "}";
                }
            }

            return content;
        }

        public string GetTextBetweenBraces(string content)
        {
            var Regex = new Regex(@"({[\S\s]*})");
            var match = Regex.Match(content);
            if (match.Success)
            {
                return match.Value;
            }
            return content;
        }
    }
}
