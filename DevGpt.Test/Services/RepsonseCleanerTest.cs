using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Console.Chatmodel;
using DevGpt.Console.Services;
using Newtonsoft.Json;

namespace DevGpt.Test.Services
{
    public class RepsonseCleanerTest
    {
        [Fact]
        public void Clean_ValidJsonDoesNothing()
        {
            // read an embedded resource called sample.json
            var content = GetEmbeddedResource("DevGpt.Test.Services.Sample.json");

            var cleaner = new ResponseCleaner();
            var result = cleaner.CleanJsonResponse(content);
            Assert.Equal(content, result);

        }

        [Fact]
        public void Clean_FixesBadResponse()
        {
            // read an embedded resource called sample.json
            var content = GetEmbeddedResource("DevGpt.Test.Services.BadResponse.json");
            var cleaner = new ResponseCleaner();
            var result = cleaner.CleanJsonResponse(content);
            // make sure the result can be parsed now
            var data = JsonConvert.DeserializeObject<AssitantReply>(result);


            Assert.NotEqual(content, result);
        }

        [Fact]
        public void SerializeJsonWithMultilineData()
        {
            var data = new JsonData
            {
                Name = "Test",
                Data = GetEmbeddedResource("DevGpt.Test.Services.TextFile.cs")
            };
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText("C:\\temp\\sample.json",json);

        }
        
        private static string GetEmbeddedResource(string devgptTestServicesSampleJson)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var names = executingAssembly.GetManifestResourceNames();
            var stream = executingAssembly.GetManifestResourceStream(devgptTestServicesSampleJson);
            var reader = new StreamReader(stream);
            var content = reader.ReadToEnd();
            return content;
        }
    }
}

public class JsonData
{
    public string Name { get; set; }
    public string Data { get; set; }
}