namespace DevGpt.Console.Tasks; 

public class DevGptTask
{
    public int id { get; set; }
    public string task { get; set; }
    public string command { get; set; }
    //public List<int> dependent_task_ids { get; set; }
    public string status { get; set; }

    public override string ToString()
    {
        
        return $"task: {task}({command},{status})";
    }
}

