namespace Order.WebAPI.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        // Kaç adet alındı
        public int ProductAmount { get; set; }

        // Hesaplanan toplam fiyat (DB'ye yazmak zorunda değilsin)
        public decimal TotalPrice { get; set; }

        // Hangi siparişe ait
        public int OrderingId { get; set; }

        // Navigation property
        public Ordering Ordering { get; set; }
    }
}
