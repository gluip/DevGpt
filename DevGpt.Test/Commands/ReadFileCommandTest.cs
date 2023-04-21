using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Console;
using DevGpt.Console.Commands;

namespace DevGpt.Test.Commands
{
    public class ReadFileCommandTest
    {
        private ReadFileCommand command;

        public ReadFileCommandTest()
        {
            command = new ReadFileCommand();
        }

        [Fact]
        public void Execute_InvalidFile_ReturnsCorrectMessage()
        {
            var result = command.Execute("some_other_file.txt");
            Assert.Contains("Could not find file",result);
        }

        [Fact]
        public void Execute_ValidFile_ReturnsContentAndFilename()
        {
            var result = command.Execute("sample.txt");
            Assert.Equal("read_file of 'sample.txt' returned 'Here is some sample content'",  result);
        }
    }
}
