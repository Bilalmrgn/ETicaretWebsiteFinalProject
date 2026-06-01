using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;

        public OrderController(IMemoryCache cache, IHttpContextAccessor httpContextAccessor, IOrderingService orderingService, AppDbContext context)
        {
            _cache = cache;
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

        [HttpGet("has-completed-order/{userId}")]
        public async Task<IActionResult> HasCompletedOrder(string userId)
        {
            var result = await _orderingService.HasCompletedOrderAsync(userId);
            return Ok(result);
        }

        [HttpPut("update-status/{orderId}/{status}")]
        public IActionResult UpdateStatus(int orderId, OrderStatus status)
        {
            var order = _context.Orderings.Find(orderId);

            if (order == null)
                return NotFound("Order not found");

            order.UpdateStatus(status);

            _context.SaveChanges();

            return Ok("Order status updated successfully");
        }

        [HttpGet("dashboard-statistics")]
        public async Task<IActionResult> GetDashboardStatistics()
        {
            var data = await _cache.GetOrCreateAsync("order-dashboard-statistics", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);

                var totalOrders = await _context.Orderings
                    .AsNoTracking()
                    .CountAsync();

                var totalIncome = await _context.Orderings
                    .AsNoTracking()
                    .SumAsync(x => x.TotalPrice);

                var topSellingProducts = await _context.OrderDetails
                    .AsNoTracking()
                    .GroupBy(x => new { x.ProductId, x.ProductName, x.ProductPrice })
                    .Select(g => new
                    {
                        ProductId = g.Key.ProductId,
                        ProductName = g.Key.ProductName,
                        Price = g.Key.ProductPrice,
                        TotalSold = g.Sum(x => x.ProductAmount)
                    })
                    .OrderByDescending(x => x.TotalSold)
                    .Take(5)
                    .ToListAsync();

                var salesByCities = await _context.Orderings
                    .AsNoTracking()
                    .Where(x => !string.IsNullOrEmpty(x.City))
                    .GroupBy(x => x.City)
                    .Select(g => new
                    {
                        City = g.Key,
                        OrderCount = g.Count(),
                        ProductCount = g.SelectMany(x => x.OrderDetails).Sum(od => od.ProductAmount)
                    })
                    .OrderByDescending(x => x.OrderCount)
                    .Take(10)
                    .ToListAsync();

                return new
                {
                    TotalOrderCount = totalOrders,
                    TotalIncome = totalIncome,
                    TopSellingProducts = topSellingProducts,
                    SalesByCities = salesByCities
                };
            });

            return Ok(data);
        }
    }
}
