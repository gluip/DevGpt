using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Models.Utils
{
    public class DevGptResourceReader
    {
        public static string GetEmbeddedResource(Assembly assembly, string resourceName)
        {

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }


        }
    }
}
