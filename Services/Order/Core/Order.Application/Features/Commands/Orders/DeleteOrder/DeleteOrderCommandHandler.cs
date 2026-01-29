using MediatR;
using Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Commands.Orders.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest, DeleteOrderCommandResponse>
    {
        private readonly IOrderingRepository _orderingRepository;
        public DeleteOrderCommandHandler(IOrderingRepository orderingRepository)
        {
            _orderingRepository = orderingRepository;
        }
        public async Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderingRepository.GetByIdAsync(request.OrderId);

            if(order == null)
            {
                throw new Exception("Order bulunamadı.(DeleteOrderCommandHandler sınıfında)");
            }

            await _orderingRepository.DeleteAsync(order);

            return new DeleteOrderCommandResponse
            {
                Message = "Order başarıyla silindi."
            };
        }
    }
}
