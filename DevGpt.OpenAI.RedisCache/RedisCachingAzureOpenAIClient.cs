
using System.Text.Json;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using static System.Net.Mime.MediaTypeNames;

namespace DevGpt.OpenAI.RedisCache
{
    public class RedisCachingAzureOpenAIClient : IDevGptOpenAIClient
    {
        private readonly IDevGptOpenAIClient _client;
        private IRedisClient _redisclient;

        public RedisCachingAzureOpenAIClient(IDevGptOpenAIClient client, IRedisClient redisclient)
        {
            _client = client;
            _redisclient = redisclient;
        }
        public async Task<DevGptChatResponse> CompletePrompt(IList<DevGptChatMessage> allMessages,
            IList<ICommandBase> commands = null)
        {
            // calculate a hash of all the messages 
            // if the hash is in the cache, return the result
            // if the hash is not in the cache, call the client and store the result in the cache
            // return the result
            var hash = GetHash(allMessages);

            // check the cache
            var cachedResult = _redisclient.GetFromCache(hash);
            if (cachedResult != null)
            {
                System.Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.WriteLine("Using cached response");
                return JsonSerializer.Deserialize<DevGptChatResponse>(cachedResult);
            }

            var completePrompt = await _client.CompletePrompt(allMessages, commands);
            _redisclient.AddToCache(hash, JsonSerializer.Serialize(completePrompt));

            return completePrompt;
        }

        private string GetHash(IList<DevGptChatMessage> allMessages)
        {
            var content = string.Join(",",allMessages.Select(c => c.ToString()));

            var textBytes = System.Text.Encoding.UTF8.GetBytes(content);
            return System.Convert.ToBase64String(textBytes);
        }
        
    }
}