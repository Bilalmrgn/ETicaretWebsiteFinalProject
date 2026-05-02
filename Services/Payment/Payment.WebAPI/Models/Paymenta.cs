namespace Payment.WebAPI.Models
{
    //bu sınıfın amacı ödeme geçmişi vs gibi detayları görmek
    public class Paymenta
    {
        public int PaymentaId { get; set; }

        public int OrderId { get; set; }
        public string UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public PaymentStatus Status { get; set; }

        public int? CreditCardId { get; set; } // hangi kartla ödendi

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
