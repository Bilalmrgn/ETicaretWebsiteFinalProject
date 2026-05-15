using Shared.RabbitMQ;

namespace Invoice.WebAPI.Publisher
{
    public interface IInvoiceCreatedPublisher
    {
        void Publish(InvoiceCreatedEvent invoiceCreatedEvent);
    }
}
