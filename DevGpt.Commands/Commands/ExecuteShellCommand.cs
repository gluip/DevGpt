using System.Diagnostics;
using System.Runtime.CompilerServices;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands;

public class ExecuteShellCommand : ICommand
{
    private string? outputData;
    private string? errorData;
    private const int Timeout = 10000;
    public string Execute(params string[] args)
    {
        
        if (args.Length < 1)
        {
            return $"{Name} requires at 1east 2 argument";
        }

        string arguments = "";
        if (args.Length >2)
        {
            arguments = args.Skip(2).Aggregate((a, b) => $"{a} {b}");
        }

        try
        {
            var command = args[1];
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WorkingDirectory = args[0];
            process.StartInfo.Arguments = $"/c {command} {arguments}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.Start();
            process.OutputDataReceived += (sender, eventArgs) => outputData += eventArgs.Data;
            process.ErrorDataReceived += (sender, eventArgs) => errorData += eventArgs.Data;
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            var startTime = DateTime.Now;
            while (!process.HasExited)
            {
                if (DateTime.Now.Subtract(startTime).TotalMilliseconds > Timeout)
                {
                    //process.Kill();
                    return $"{Name} still running. Output up and until now {outputData}";
                }
                Thread.Sleep(1000);
            }
            return $"the command {Name} of '{command} {arguments}' returned '{outputData}' and '{errorData}'";
        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }
    public string Name => "execute_shell";
    public string Description => "executes a shell command";
    public string[] Arguments => new[] { "workingdirectory","command","command arguments"};
}