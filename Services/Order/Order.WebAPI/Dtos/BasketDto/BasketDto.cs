namespace Order.WebAPI.Dtos.BasketDto
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public List<BasketItemDto> BasketItems { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }
}
