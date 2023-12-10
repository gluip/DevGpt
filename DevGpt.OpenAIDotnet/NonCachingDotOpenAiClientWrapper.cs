using DevGpt.OpenAI.RedisCache;
using OpenAI.Chat;

namespace DevGpt.OpenAI;

public class NonCachingDotOpenAiClientWrapper : IDotnetOpenAiClientChatEndpoint
{
    private readonly ChatEndpoint _innerChatEndpoint;
        
    public NonCachingDotOpenAiClientWrapper(ChatEndpoint innerChatEndpoint)
    {
        _innerChatEndpoint = innerChatEndpoint;
    }

    public async Task<ChatResponse> GetCompletionAsync(ChatRequest chatRequest,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return await _innerChatEndpoint.GetCompletionAsync(chatRequest, cancellationToken);
    }
}