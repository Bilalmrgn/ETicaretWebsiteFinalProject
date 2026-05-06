using Payment.WebAPI.Dtos.PaymentDtos;

namespace Payment.WebAPI.Services.Interface
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto dto);
        Task NotifyOrderSuccess(int orderId);
    }
}
