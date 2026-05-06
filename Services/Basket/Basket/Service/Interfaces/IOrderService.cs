namespace Basket.Service.Interfaces
{
    public interface IOrderService
    {
        Task<bool> AnyCompletedOrderAsync(string userId);
    }
}
