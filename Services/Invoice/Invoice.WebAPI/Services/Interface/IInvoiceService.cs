using Invoice.WebAPI.Model;

namespace Invoice.WebAPI.Services.Interface
{
    public interface IInvoiceService
    {
        Task CreateAsync(InvoiceModel invoice);

        Task<InvoiceModel?> GetByOrderIdAsync(int orderId);
    }
}
