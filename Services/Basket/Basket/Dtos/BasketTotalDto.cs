namespace Basket.Dtos
{
    //sepet total tutar
    public class BasketTotalDto
    {
        public string? UserId { get; set; }
        public string? DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        public List<BasketItemDto> BasketItems { get; set; } = new();
        public Decimal TotalPrice { get; set; }
    }
}