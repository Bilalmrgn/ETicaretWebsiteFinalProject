namespace Order.WebAPI.Dtos.OrderDetailDto
{
    public class CreateOrderDetailDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductAmount { get; set; }
    }
}
