using StackExchange.Redis;

namespace DevGpt.OpenAI.RedisCache;

public interface IRedisClient{
    string? GetFromCache(string hash);
    void AddToCache(string hash, string data);
}

public class RedisClient : IRedisClient
{
    public string? GetFromCache(string hash)
    {
        var db = GetDatabase();
        var redisValue = db.HashGet(new RedisKey(hash), new RedisValue("value"));
        return redisValue.HasValue ? redisValue.ToString() : null;
    }

    public void AddToCache(string hash, string data)
    {
        //add a hash to the local redis
        var db = GetDatabase();
        db.HashSet(hash,new HashEntry[]{new HashEntry("value", data)});
    }

    private static IDatabase GetDatabase()
    {
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();
        return db;
    }
}