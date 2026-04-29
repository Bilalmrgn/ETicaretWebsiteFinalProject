namespace Order.WebAPI.Models
{
    public class Ordering
    {
        public int OrderingId { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatus Status { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new();

        public Ordering()
        {
            CreatedDate = DateTime.UtcNow; // otomatik tarih
            Status = OrderStatus.Pending;  // ilk durum
        }

        public void Complete()
        {
            Status = OrderStatus.Completed;
        }
        // Ödeme başarısız olduğunda çağrılır
        public void Fail()
        {
            Status = OrderStatus.Failed;
        }

        // Ödeme başlatıldığında
        public void SetPaymentProcessing()
        {
            Status = OrderStatus.Processing;
        }

    }
}
