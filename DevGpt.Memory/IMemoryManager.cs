namespace DevGpt.Memory;

public interface IMemoryManager
{
    void StoreMessage(string message);
    IList<string> RetrieveRelevantMessages(string message,int count =1);
}