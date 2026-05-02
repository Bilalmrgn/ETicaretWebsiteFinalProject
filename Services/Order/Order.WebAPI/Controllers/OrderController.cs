using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.WebAPI.Dtos.OrderingDtos;
using Order.WebAPI.Services.Interfaces;

namespace Order.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderingService _orderingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderController(IHttpContextAccessor httpContextAccessor,IOrderingService orderingService)
        {
            _httpContextAccessor = httpContextAccessor;
            _orderingService = orderingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
                await _orderingService.CreateOrderAsync(createOrderDto);
                return Ok("Sipariş başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderingService.GetAllOrderAsync();
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetailByOrderId(int orderId)
        {
            var orderDetails = await _orderingService.GetAllOrderDetailByOrderId(orderId);
            return Ok(orderDetails);
        }
    }
}
