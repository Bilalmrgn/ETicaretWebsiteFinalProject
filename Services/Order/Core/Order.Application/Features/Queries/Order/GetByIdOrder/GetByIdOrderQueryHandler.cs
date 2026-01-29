using MediatR;
using Order.Application.Features.Queries.Order.DTO;
using Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Queries.Order.GetByIdOrder
{
    public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQueryRequest, GetByIdOrderQueryResponse>
    {
        private readonly IOrderingRepository _orderingRepository;
        public GetByIdOrderQueryHandler(IOrderingRepository orderingRepository)
        {
            _orderingRepository = orderingRepository;
        }
        public async Task<GetByIdOrderQueryResponse> Handle(GetByIdOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderingRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                throw new Exception("Sipariş bulunamadı");
            }

            return new GetByIdOrderQueryResponse
            {
                OrderingId = order.OrderingId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                UserId = order.UserId,
                OrderDetails = order.OrderDetails.Select(d => new OrderDetailDto
                {
                    OrderDetailId = d.OrderDetailId,
                    ProductAmount = d.ProductAmount,
                    ProductPrice = d.ProductPrice,
                    ProductTotalPrice = d.ProductTotalPrice,
                    ProductId = d.ProductId,
                    ProductName = d.ProductName
                }).ToList(),
            };
        }
    }
}
