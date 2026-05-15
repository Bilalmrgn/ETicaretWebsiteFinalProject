using Invoice.WebAPI.Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Invoice.WebAPI.Document
{
    public class InvoiceDocument : IDocument
    {
        private readonly InvoiceModel _invoice;

        public InvoiceDocument(InvoiceModel invoice)
        {
            _invoice = invoice;
        }

        public DocumentMetadata GetMetadata()
        {
            return DocumentMetadata.Default;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);

                // HEADER
                page.Header()
                    .Text("INVOICE")
                    .FontSize(30)
                    .Bold()
                    .FontColor(Colors.Blue.Medium);

                // CONTENT
                page.Content()
                    .PaddingVertical(20)
                    .Column(column =>
                    {
                        column.Spacing(10);

                        column.Item()
                            .Text($"Invoice Id: {_invoice.Id}");

                        column.Item()
                            .Text($"User Id: {_invoice.UserId}");

                        column.Item()
                            .Text($"Total Price: {_invoice.TotalPrice} ₺");

                        column.Item()
                            .Text($"Date: {_invoice.CreatedDate}");
                    });

                // FOOTER
                page.Footer()
                    .AlignCenter()
                    .Text("Thank you for your purchase");
            });
        }
    }
}
