using System.Text.Json;
using DevGpt.Console.Tasks;

namespace DevGpt.Taskbased.Test.Tasks;

public class DeveloperTest
{
    [Fact]
    public void TestDeserialze()
    {
        var text =
            "{\"id\":1,\"task\":\"Create a new project with force\",\"command\":\"execute_shell\",\"dependent_task_ids\":[0],\"status\":\"completed\",\"arguments\":[\"dotnet new console -o myApp --force\"],\"result\":\"The template 'Console App' was created successfully. Processing post-creation actions... Restoring C:\\git\\DevGpt\\DevGpt.Taskbased\\bin\\Debug\\net6.0\\myApp\\myApp.csproj: Determining projects to restore... All projects are up-to-date for restore. Restore succeeded.\"}";
        
        text = text.Replace($"\\","\\\\");
        var task = JsonSerializer.Deserialize<DevGptTask>(text,new JsonSerializerOptions{});
        
    }
}