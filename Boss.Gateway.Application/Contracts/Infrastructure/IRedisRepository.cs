namespace Boss.Gateway.Application.Contracts.Persistence;


public interface IRedisRepository  {
    Task<string> GetStringAsync(string key);
    Task<string> SetStringAsync(string key, string value);
    Task<string> DeleteAsync(string key);
    Task AddToSetAsync(string key, string? value, string[]? values);
    Task<string[]> GetSetMembersAsync(string key);

}