using MediatR;
using Order.Application.Interfaces;
using Order.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Commands.Orders.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommandRequest, UpdateOrderCommandResponse>
    {
        private readonly IOrderingRepository _orderingRepository;

        public UpdateOrderCommandHandler(IOrderingRepository orderingRepository)
        {
            _orderingRepository = orderingRepository;
        }
        public async Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderingRepository.GetByIdAsync(request.OrderingId);

            if (order == null)
            {
                throw new Exception("Güncellenmek için sipariş bulunamadı.(UpdateOrderCommandHandler sınıfında)");
            }
            //eski verileri temizle ve bunlar yerine başka bir şey yazacağız
            order.OrderDetails.Clear();

            order.OrderDate = DateTime.Now;
            order.TotalPrice = request.TotalPrice;
            foreach (var item in request.OrderDetails)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    ProductAmount = item.ProductAmount,
                    ProductName = item.ProductName,
                    ProductTotalPrice = item.TotalPrice,
                    ProductPrice = item.ProductPrice
                });
            }

            await _orderingRepository.UpdateAsync(order);

            return new UpdateOrderCommandResponse
            {
                Message = "Order Başarıyla Güncellendi"
            };
        }
    }
}
