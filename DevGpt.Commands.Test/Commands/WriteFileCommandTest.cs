using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Console.Commands;

namespace DevGpt.Commands.Test.Commands
{
    public class WriteFileCommandTest
    {
        [Fact]
        public void Execute_WithNonExistingPath_CreatesPath()
        {
            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(),Guid.NewGuid().ToString()+".txt");
            var command = new WriteFileCommand();
            command.Execute(new[] { path, "test" });
            Assert.True(File.Exists(path));
            File.Delete(path);
         
        }
    }
}
