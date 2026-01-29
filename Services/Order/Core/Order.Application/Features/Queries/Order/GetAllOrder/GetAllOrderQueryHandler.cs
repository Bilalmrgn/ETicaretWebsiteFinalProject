using Order.Domain;
using MediatR;
using Order.Application.Features.Queries.Order.DTO;
using Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Queries.Order.GetAllOrder
{
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQueryRequest, List<GetAllOrderQueryResponse>>
    {
        private readonly IOrderingRepository _orderingRepository;
        public GetAllOrderQueryHandler(IOrderingRepository orderingRepository)
        {
            _orderingRepository = orderingRepository;
        }

        public async Task<List<GetAllOrderQueryResponse>> Handle(GetAllOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var addresses = await _orderingRepository.GetAllAsync();

            var response = addresses.Select(a => new GetAllOrderQueryResponse
            {
                OrderingId = a.OrderingId,
                UserId = a.UserId,
                TotalPrice = a.TotalPrice,
                OrderDate = a.OrderDate,
                OrderDetails = a.OrderDetails.Select(d => new OrderDetailDto
                {
                    OrderDetailId = d.OrderDetailId,
                    ProductAmount = d.ProductAmount,
                    ProductPrice = d.ProductPrice,
                    ProductName = d.ProductName,
                    ProductId = d.ProductId,
                    ProductTotalPrice = d.ProductTotalPrice
                }).ToList()
            }).ToList();

            return response;
        }
    }
}
