using Order.WebAPI.Dtos.OrderDetailDto;
using Order.WebAPI.Dtos.OrderingDtos;

namespace Order.WebAPI.Services.Interfaces
{
    public interface IOrderingService
    {
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<List<ResultOrderDto>> GetAllOrderAsync();
        Task<ResultOrderDto> GetAllOrderDetailByOrderId(int orderId);
        Task<List<ResultOrderDto>> GetAllOrderByUserIdAsync();


    }
}
