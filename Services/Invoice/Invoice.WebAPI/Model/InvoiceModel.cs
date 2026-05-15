namespace Invoice.WebAPI.Model
{
    public class InvoiceModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string UserId { get; set; } = default!;

        public string Email { get; set; } = default!;

        public decimal TotalPrice { get; set; }

        public string InvoiceNumber { get; set; } = default!;

        public string PdfPath { get; set; } = default!;

        public DateTime CreatedDate { get; set; }
    }
}
