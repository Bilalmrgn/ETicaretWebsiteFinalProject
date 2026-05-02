namespace Payment.WebAPI.Models
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }

        public string UserId { get; set; }

        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
