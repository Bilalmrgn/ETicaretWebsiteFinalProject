namespace Basket.Service.Interfaces
{
    public interface IRedisService
    {
        Task SetAsync(string key, string value);
        Task<string?> GetAsync(string key);
        Task RemoveAsync(string key);
    }
}
