namespace DevGpt.Models;

public interface IMagicFunction
{
    Task<T> GetResults<T>(string question, string context, T example);
}