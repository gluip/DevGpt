using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Console.Commands;

namespace DevGpt.Commands.Test
{
    public class ExecuteShellCommandTest
    {
        [Fact]
        public void Running_NpmRunDev_IsOk()
        {
            var command = new ExecuteShellCommand();
            var result = command.Execute(new[]{"C:\\DevGpt\\Work\\age_calculator", "npm run dev" });
            Assert.True(result.Contains("vite"));
        }

        [Fact]
        public void Running_Dir_IsOk()
        {
            var command = new ExecuteShellCommand();
            var result = command.Execute(new[] { "C:\\DevGpt\\Work\\age_calculator", "dir" });
            Assert.True(result.Contains("age_calculator"));
        }

        [Fact]
        public void Running_UnknownCommand_IsOk()
        {
            var command = new ExecuteShellCommand();
            var result = command.Execute(new[] { "C:\\DevGpt\\Work\\age_calculator", "disr" });
            Assert.True(result.Contains("not recognized"));
        }
    }
}
