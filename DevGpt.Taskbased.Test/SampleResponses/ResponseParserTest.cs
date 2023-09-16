using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Console.Tasks;
using DevGpt.Models.Utils;

namespace DevGpt.Test.SampleResponses
{
    public class ResponseParserTest
    {
        [Fact]
        public void GetTaskList_CorrectInput_ParsesOk()
        {
            var responseParser = new ResponseParser();
            var response = DevGptResourceReader.GetEmbeddedResource(Assembly.GetExecutingAssembly(),
                "DevGpt.Taskbased.Test.SampleResponses.SampleResponse1.txt");

            var tasklist = responseParser.GetTaskList(response);
            Assert.NotNull(tasklist);
            Assert.Equal(3, tasklist.Length);
        }

        [Fact]
        public void GetTaskList_Sample2CorrectInput_ParsesOk()
        {
            var responseParser = new ResponseParser();
            var response = DevGptResourceReader.GetEmbeddedResource(Assembly.GetExecutingAssembly(),
                "DevGpt.Taskbased.Test.SampleResponses.SampleResponse2.txt");

            var tasklist = responseParser.GetTaskList(response);
            Assert.NotNull(tasklist);
            Assert.Equal(3, tasklist.Length);
        }
    }
}
