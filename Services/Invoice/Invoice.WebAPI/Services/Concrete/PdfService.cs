using Invoice.WebAPI.Document;
using Invoice.WebAPI.Model;
using Invoice.WebAPI.Services.Interface;
using QuestPDF.Fluent;

namespace Invoice.WebAPI.Services.Concrete
{
    public class PdfService : IPdfService
    {
        public byte[] GeneratePdf(InvoiceModel invoice)
        {
            var document = new InvoiceDocument(invoice);

            return document.GeneratePdf();
        }
    }
}
