namespace DevGpt.Memory;

public class Memory
{
    public string message { get; set; }
    public IReadOnlyList<float> embedding { get; set; }
}