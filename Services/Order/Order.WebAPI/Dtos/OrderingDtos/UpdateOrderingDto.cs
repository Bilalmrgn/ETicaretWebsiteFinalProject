namespace Order.WebAPI.Dtos.OrderingDtos
{
    public class UpdateOrderingDto
    {
        public int OrderingId { get; set; }
        public int AddressId { get; set; }
        public int OrderStatus { get; set; }
    }
}
