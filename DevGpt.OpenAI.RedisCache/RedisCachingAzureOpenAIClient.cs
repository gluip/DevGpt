using Azure.AI.OpenAI;
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
        public async Task<string> CompletePrompt(IList<DevGptChatMessage> allMessages)
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
                return cachedResult;
            }

            var completePrompt = await _client.CompletePrompt(allMessages);
            _redisclient.AddToCache(hash, completePrompt);

            return completePrompt;
        }

        private string GetHash(IList<DevGptChatMessage> allMessages)
        {
            var content = string.Join(",",allMessages.Select(c => c.Content + c.Role.ToString()));

            var textBytes = System.Text.Encoding.UTF8.GetBytes(content);
            return System.Convert.ToBase64String(textBytes);
        }
        
    }
}