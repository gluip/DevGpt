using System.Text.Json.Serialization;

namespace DevGpt.Console.Tasks; 

public class DevGptTask
{
    public int id { get; set; }
    public string task { get; set; }
    public string command { get; set; }

    //public List<int> dependent_task_ids { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaskStatus status { get; set; } = TaskStatus.pending;//"pending";
    public string[] arguments { get; set; }

    public string reason { get; set; }

    public string result { get; set; } = "not yet run";

    public override string ToString()
    {
        return $"{task}({command},{status})";
    }
}

public enum TaskStatus
{
    pending,
    failed,
    needtorefine,
    completed,
    
}