using System.Text.Json;
using System.Text.RegularExpressions;

namespace DevGpt.Console.Tasks;

public class ResponseParser : IResponseParser
{
    public DevGptTask[] GetTaskList(string textResponse)
    {
        var taskJson = Regex.Match(textResponse, @"TASK_LIST=\[([\s\S]*?)\] ###END###", RegexOptions.Multiline).Value;
        taskJson = taskJson.Replace("TASK_LIST=", "");
        taskJson = taskJson.Replace("###END###", "").Trim();
        //taskJson = taskJson.Replace($"\\", "\\\\");

        return JsonSerializer.Deserialize<DevGptTask[]>(taskJson);
    }
}