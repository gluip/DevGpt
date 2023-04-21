using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Console.Commands;

namespace DevGpt.Test.Commands
{
    public class CommandExtensionsTest
    {
        [Fact]
        public void GetHelp_ReturnsCorrectMessage()
        {
            var command = new ReadFileCommand();
            var result = command.GetHelp();
            Assert.Equal("\"read_file\" args: \"path\" - reads a file", result);
        }
    }
}
