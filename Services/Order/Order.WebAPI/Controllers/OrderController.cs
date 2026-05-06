using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.WebAPI.Context;
using Order.WebAPI.Dtos.OrderingDtos;
using Order.WebAPI.Models;
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
        private readonly AppDbContext _context;
        public OrderController(IHttpContextAccessor httpContextAccessor,IOrderingService orderingService,AppDbContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _orderingService = orderingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
                var order = await _orderingService.CreateOrderAsync(createOrderDto);
                return Ok(new
                {
                    orderId = order.OrderingId
                });
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

        [HttpGet("GetMyOrders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var values = await _orderingService.GetAllOrderByUserIdAsync();
            return Ok(values);
        }

        [HttpPost("complete/{orderId}")]
        public IActionResult CompleteOrder(int orderId)
        {
            var order = _context.Orderings.Find(orderId);

            if (order == null)
                return NotFound("Order not found");

            // zaten ödenmiş mi kontrol
            if (order.Status == OrderStatus.Completed)
                return BadRequest("Order already completed");

            order.Complete();

            _context.SaveChanges();

            return Ok("Order completed successfully");
        }
    }
}
