using Azure;
using Azure.AI.OpenAI;

namespace DevGpt.Memory;

public class RamMemoryManager : IMemoryManager
{
    private IList<Memory> memories = new List<Memory>();

    public void StoreMessage(string message)
    {
        //return;
        var embedding = GetEmbedding(message);

        memories.Add(new Memory
        {
            message = message,
            embedding = embedding
        });
    }

    private static IReadOnlyList<float> GetEmbedding(string message)
    {
        var azureKey = Environment.GetEnvironmentVariable("DevGpt_EmbeddingAzureKey", EnvironmentVariableTarget.User);
        var uri = Environment.GetEnvironmentVariable("DevGpt_EmbeddingAzureUri", EnvironmentVariableTarget.User);


        AzureKeyCredential credentials = new(azureKey);

        OpenAIClient openAIClient = new(new Uri(uri), credentials);

        EmbeddingsOptions embeddingOptions = new(message);

        var deploymentName = "text-embedding-ada-002";

        var returnValue = openAIClient.GetEmbeddings(deploymentName, embeddingOptions);
        return returnValue.Value.Data[0].Embedding;
    }

    public IList<string> RetrieveRelevantMessages(string message,int count =1)
    {
        var embedding = GetEmbedding(message);
        var relevantMemory = memories.OrderByDescending(m => GetDotProduct(m.embedding, embedding));
        return relevantMemory.Select(m => m.message).Take(count).ToList();
    }

    private object GetDotProduct(IReadOnlyList<float> memory, IReadOnlyList<float> embedding)
    {
        var distance = 0.0;
        for (int i = 0; i < memory.Count; i++)
        {
            distance += memory[i] * embedding[i];
        }
        return distance;
    }
}