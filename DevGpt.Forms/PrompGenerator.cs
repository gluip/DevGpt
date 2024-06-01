using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Models;

namespace DevGpt.Forms
{
    internal class PrompGenerator
    {
        public string GetFullPrompt(string userPrompt)
        {
            return userPrompt + Environment.NewLine + GetGenericPromt();
        }
        protected string GetGenericPromt()
        {
            return EmbeddedResourceReader.GetEmbeddedResourceText("DevGpt.Forms.MainPrompt.txt");
        }
    }
}
