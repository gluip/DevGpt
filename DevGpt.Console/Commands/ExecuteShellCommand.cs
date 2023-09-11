using System.Diagnostics;
using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands;

public class ExecuteShellCommand : ICommand
{
    public string Execute(params string[] args)
    {
        if (args.Length < 1)
        {
            return $"{Name} requires at 1east argument";
        }

        string arguments = "";
        if (args.Length >1)
        {
            arguments = args.Skip(1).Aggregate((a, b) => $"{a} {b}");
        }

        try
        {
            var command = args[0];
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = $"/c {command} {arguments}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit();
            return $"the command {Name} of '{command} {arguments}' returned '{output}' and '{error}'";
        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }
    public string Name => "execute_shell";
    public string Description => "executes a shell command";
    public string[] Arguments => new[] {"command"};
}