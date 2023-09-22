namespace DevGpt.OpenAI.RedisCache.Test
{
    public class RedisClientTest
    {
        [Fact]
        public void AddToCache_WithValidData_AddsToCache()
        {
            var client = new RedisClient();
            var key = "test";
            var value = "testdata";
            client.AddToCache(key, value);


        }

        [Fact]
        public void GetFromCache_WithValidData_GetsFromCache()
        {
            var client = new RedisClient();
            var key = "test";
            var value = "testdata";
            client.AddToCache(key, value);
            var result = client.GetFromCache(key);
            Assert.Equal(value, result);
        }

        [Fact]
        public void GetFromCache_WithNoData_ReturnsNull()
        {
            var client = new RedisClient();
            var key = "unknown_test";
            var result = client.GetFromCache(key);
            Assert.Null(result);
            
        }
    }
}