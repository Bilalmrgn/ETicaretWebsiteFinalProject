using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Order.WebAPI.Context;
using Order.WebAPI.Dtos.BasketDto;
using Order.WebAPI.Dtos.OrderDetailDto;
using Order.WebAPI.Dtos.OrderingDtos;
using Order.WebAPI.Models;
using Order.WebAPI.Services.Interfaces;
using System.Security.Claims;
using System.Text.Json;

namespace Order.WebAPI.Services.Concrete
{
    public class OrderingService : IOrderingService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        public OrderingService(AppDbContext context, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not authenticated.");
            }

            //kullanýcýdan gelen token'i al ve baþka servise aynen ilet
            var client = _httpClientFactory.CreateClient();

            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.GetAsync("https://localhost:7178/api/Basket");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create order.");
            }

            var jsonData = await response.Content.ReadAsStringAsync();

            var basket = JsonConvert.DeserializeObject<BasketDto>(jsonData);

            if (basket == null || !basket.BasketItems.Any())
                throw new Exception("Sepet boþ");

            var order = new Ordering
            {
                UserId = userId,
                
                TotalPrice = basket.TotalPrice,
                FirstName = createOrderDto.FirstName,
                LastName = createOrderDto.LastName,
                PhoneNumber = createOrderDto.PhoneNumber,
                City = createOrderDto.City,
                District = createOrderDto.District,
                AddressDetail = createOrderDto.AddressDetail,
            };

            foreach (var item in basket.BasketItems)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductPrice = item.Price,
                    ProductAmount = item.Quantity,
                });
            }
            await _context.Orderings.AddAsync(order);
            await _context.SaveChangesAsync();

            return;

        }

        public async Task<List<ResultOrderDto>> GetAllOrderAsync()
        {
            var orders = await _context.Orderings
                .Include(x => x.OrderDetails)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new ResultOrderDto
                {
                    OrderingId = x.OrderingId,
                    TotalPrice = x.TotalPrice,
                    CreatedDate = x.CreatedDate,
                    Status = x.Status,

                    City = x.City,
                    District = x.District,
                    AddressDetail = x.AddressDetail,

                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,

                    OrderDetails = x.OrderDetails.Select(d => new ResultOrderDetailDto
                    {
                        ProductId = d.ProductId,
                        ProductName = d.ProductName,
                        ProductPrice = d.ProductPrice,
                        ProductAmount = d.ProductAmount
                    }).ToList()
                }).ToListAsync();
            return orders;
        }
    }
}
