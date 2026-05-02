namespace Frontend.DtosLayer.PaymentDtos.CreditCardDtos
{
    public class ResultCreditCardDto
    {
        public int CreditCardId { get; set; }

        public string UserId { get; set; }

        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpireDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
