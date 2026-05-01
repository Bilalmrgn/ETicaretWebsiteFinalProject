namespace Order.WebAPI.Models
{
    public class Ordering
    {
        public int OrderingId { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }//basketten gelecek
        public DateTime CreatedDate { get; set; }
        public OrderStatus Status { get; set; }

        //adres ile ilgili bilgiler
        public string City { get; set; }
        public string District { get; set; }
        public string AddressDetail { get; set; }

        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

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
