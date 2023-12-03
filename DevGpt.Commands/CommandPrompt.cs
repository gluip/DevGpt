using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevGpt.Commands
{
    public interface ICommandPrompt
    {
        void Open();
        string ReadOutput();
        void SendRightArrowKey();
        void SendLeftArrowKey();
        void SendEnterKey();
        void SendKeys(string keys);
        void SendLine(string line);
    }

    public class CommandPrompt : ICommandPrompt
    {
        private Process process;
        private StringBuilder outputBuilder = new StringBuilder();
        private StringBuilder errorOutput = new StringBuilder();

        public void Open()
        {
            process = new Process();
            //process.StartInfo.FileName = fileName;
            
            //process.StartInfo.Arguments = arguments;
            process.StartInfo.FileName = "cmd.exe"; 
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;

            process.ErrorDataReceived += (sender, args) => errorOutput.AppendLine(args.Data);
            
            process.Start();
            process.BeginErrorReadLine();

            Task.Run(() =>
            {
                while (!process.HasExited)
                {
                    int output = process.StandardOutput.Read();
                    while (output != -1)
                    {
                        //convert int to unicode char and add to output
                        var value = Convert.ToChar(output);
                        outputBuilder.Append(value);
                        //outputBuilder.Append((char)output);
                        //System.Console.Write((char)output);
                        output = process.StandardOutput.Read();
                    }
                }
            });

               
        }
        static string RemoveColorEscapeSequences(string input)
        {
            input= input.Replace("\u001b[4m", "[SELECTED]");
            string pattern = @"\u001b\[\d+m";
            string replacement = string.Empty;

            string cleanedString = Regex.Replace(input, pattern, replacement);
            cleanedString = cleanedString.Replace("\u001b[2K", String.Empty)
                .Replace("\u001b[1G",string.Empty)
                .Replace("\u001b7",string.Empty)
                .Replace("\u001b8",string.Empty);

            return cleanedString;
        }

        public string ReadOutput()
        {
            // clean it using regex : /[\u001b\u009b][[()#;?]*(?:[0-9]{1,4}(?:;[0-9]{0,4})*)?[0-9A-ORZcf-nqry=><]/g
            Thread.Sleep(1000);
            return RemoveColorEscapeSequences(outputBuilder.ToString());

        }

        public void SendLine(string line)
        {
            process.StandardInput.WriteLine(line);
        }
        

        public void SendRightArrowKey()
        {
            //write right arrow key
            process.StandardInput.Write("\u001b[C");
        }

        public void SendLeftArrowKey()
        {
            //write right arrow key
            process.StandardInput.Write("\u001b[D");
        }

        public void SendEnterKey()
        {
            //write right arrow key
            process.StandardInput.Write("\r\n");
        }

        public void SendKeys(string keys)
        {
            //write right arrow key
            process.StandardInput.Write(keys);
        }

        public void Close()
        {
            process.Close();
        }
        
    }
}
