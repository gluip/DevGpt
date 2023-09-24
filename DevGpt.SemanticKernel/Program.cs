// See https://aka.ms/new-console-template for more information
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Skills.Core;

Console.WriteLine("Hello, World!");



var AzureOpenAIDeploymentName = "gpt-4";
var AzureOpenAIEndpoint = Environment.GetEnvironmentVariable("DevGpt_AzureUri", EnvironmentVariableTarget.User);
var AzureOpenAIApiKey = Environment.GetEnvironmentVariable("DevGpt_AzureKey", EnvironmentVariableTarget.User);

var kernel = Kernel.Builder
    .WithAzureTextEmbeddingGenerationService("text-embedding-ada-002", AzureOpenAIEndpoint, AzureOpenAIApiKey)
    .WithAzureChatCompletionService(
        AzureOpenAIDeploymentName,  // The name of your deployment (e.g., "gpt-35-turbo")
        AzureOpenAIEndpoint,        // The endpoint of your Azure OpenAI service
        AzureOpenAIApiKey           // The API key of your Azure OpenAI service
    ).WithMemoryStorage(new VolatileMemoryStore())
    .Build();

const string MemoryCollectionName = "aboutMe";

await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info1", text: "My name is Andrea");
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info2", text: "I currently work as a tourist operator");
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info3", text: "I currently live in Seattle and have been living there since 2005");
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info4", text: "I visited France and Italy five times since 2015");
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info5", text: "My family is from New York");

var questions = new[]
{
    "what is my name?",
    "where do I live?",
    "where is my family from?",
    "where have I travelled?",
    "what do I do for work?",
};

foreach (var q in questions)
{
    var response = await kernel.Memory.SearchAsync(MemoryCollectionName, q).FirstOrDefaultAsync();
    Console.WriteLine(q + " " + response?.Metadata.Text);
}

