using Invoice.WebAPI.Model;

namespace Invoice.WebAPI.Services.Interface
{
    public interface IPdfService
    {
        byte[] GeneratePdf(InvoiceModel invoice);

    }
}
