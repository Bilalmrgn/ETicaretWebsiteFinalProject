using Invoice.WebAPI.Context;
using Invoice.WebAPI.Model;
using Invoice.WebAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Invoice.WebAPI.Services.Concrete
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _context;

        public InvoiceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(InvoiceModel invoice)
        {
            await _context.Invoices.AddAsync(invoice);

            await _context.SaveChangesAsync();
        }

        public async Task<InvoiceModel?> GetByOrderIdAsync(int orderId)
        {
            return await _context.Invoices.FirstOrDefaultAsync(x => x.OrderId == orderId);
        }
    }
}
