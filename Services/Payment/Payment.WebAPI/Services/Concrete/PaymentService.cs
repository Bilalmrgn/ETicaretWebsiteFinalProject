using Payment.WebAPI.Context;
using Payment.WebAPI.Dtos.PaymentDtos;
using Payment.WebAPI.Services.Interface;
using Shared.RabbitMQ;

namespace Payment.WebAPI.Services.Concrete
{

    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RabbitMqPublisher _rabbitMqPublisher;

        public PaymentService(RabbitMqPublisher rabbitMqPublisher,AppDbContext context, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _rabbitMqPublisher = rabbitMqPublisher;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task NotifyOrderSuccess(int orderId)
        {
            var client = _httpClientFactory.CreateClient();

            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
            }

            var response = await client.PostAsync($"https://localhost:7296/api/Order/complete/{orderId}", null);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Order service update failed");
            }
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto dto)
        {
            var card = _context.CreditCards.FirstOrDefault(x => x.CreditCardId == dto.CreditCardId);

            if (card == null)
            {
                return new PaymentResponseDto
                {
                    IsSuccess = false,
                    Message = "Credit card not found."
                };
            }

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (card.UserId != userId)
            {
                return new PaymentResponseDto
                {
                    IsSuccess = false,
                    Message = "Unauthorized access to credit card."
                };
            }

            var paymentEvent = new PaymentCompletedEvent
            {
                OrderId = dto.OrderId,
                UserId = userId!,
                Email = dto.Email,
                TotalPrice = dto.TotalPrice,
                PaymentDate = DateTime.UtcNow
            };

            //publisher
            _rabbitMqPublisher.PublishPaymentCompleted(paymentEvent);

            await NotifyOrderSuccess(dto.OrderId);

            return new PaymentResponseDto
            {
                IsSuccess = true,
                Message = "Payment successful"
            };
        }
    }
}
