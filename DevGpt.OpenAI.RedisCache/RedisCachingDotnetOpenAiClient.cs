using System.Text.Json;
using DevGpt.Models.OpenAI;
using OpenAI.Chat;

namespace DevGpt.OpenAI.RedisCache;

public interface IDotnetOpenAiClientChatEndpoint
{
    Task<ChatResponse> GetCompletionAsync(ChatRequest chatRequest, CancellationToken cancellationToken = new CancellationToken());
}

public class RedisCachingDotnetOpenAiClient : IDotnetOpenAiClientChatEndpoint
{
    private readonly ChatEndpoint _innerChatEndpoint;
    private IRedisClient _redisclient;

    public RedisCachingDotnetOpenAiClient(ChatEndpoint innerChatEndpoint, IRedisClient redisclient)
    {
        _innerChatEndpoint = innerChatEndpoint;
        _redisclient = redisclient;
    }

    public async Task<ChatResponse> GetCompletionAsync(ChatRequest chatRequest, CancellationToken cancellationToken = new CancellationToken())
    {
        var hash = GetHash(chatRequest);
        var cachedResult = _redisclient.GetFromCache(hash);
        if (cachedResult != null)
        {
            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("Using cached response");
            return JsonSerializer.Deserialize<ChatResponse>(cachedResult);
        }

        var result = await _innerChatEndpoint.GetCompletionAsync(chatRequest);
        _redisclient.AddToCache(hash, JsonSerializer.Serialize(result));

        return result;
    }

    private string GetHash(ChatRequest chatRequest)
    {
        var content = JsonSerializer.Serialize(chatRequest);

        var textBytes = System.Text.Encoding.UTF8.GetBytes(content);
        return System.Convert.ToBase64String(textBytes);
    }
}