using Boss.Gateway.Application.Contracts.Persistence;
using StackExchange.Redis;

namespace Boss.Gateway.Persistence.Repositories;


public class RedisRepository : IRedisRepository
{

    private readonly IDatabase _redisDb;

    public RedisRepository(IDatabase database)
    {
        _redisDb = database;
    }

    public async Task AddToSetAsync(string key, string? value, string[]? values)
    {
        if (value != null)
        {
            await _redisDb.SetAddAsync(key, value);
            return;
        }
        else
        {
            await _redisDb.SetAddAsync(key, values!.Select(m => (RedisValue)m).ToArray());
            return;
        }

    }

    

    public Task<string> DeleteAsync(string key)
    {
        throw new NotImplementedException();
    }

    public async Task<string[]> GetSetMembersAsync(string key)
    {
        var members = await _redisDb.SetMembersAsync(key);
        if (members == null || members.Length < 0)
        {
            return new string[0];
        }
        else
        {
            return members.Select(m => (string)m!).ToArray();
        }

    }

    public async Task<string> GetStringAsync(string key)
    {
        var value = await _redisDb.StringGetAsync(key);
        return value.HasValue ? value.ToString() : string.Empty;
    }

    public async Task<string> SetStringAsync(string key, string value)
    {
        try
        {
            await _redisDb.StringSetAsync(key, value);
            return "Success";
        }
        catch
        {
            throw new Exception("Error setting value");
        }
    }
}