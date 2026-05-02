namespace Frontend.DtosLayer.PaymentDtos.CreditCardDtos
{
    public class CreateCreditCardDto
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }

        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
    }
}
