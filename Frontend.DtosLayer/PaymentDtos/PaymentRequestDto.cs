namespace Frontend.DtosLayer.PaymentDtos
{
    public class PaymentRequestDto
    {
        public int OrderId { get; set; }

        public int CreditCardId { get; set; }

        public string Email { get; set; } = default!;

        public decimal TotalPrice { get; set; }
    }
}
