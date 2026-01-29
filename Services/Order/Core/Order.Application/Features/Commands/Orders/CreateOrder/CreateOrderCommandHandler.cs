using MediatR;
using Order.Application.Interfaces;
using System;
using Order.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.Features.Commands.Orders.Dto;

namespace Order.Application.Features.Commands.Orders.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        private readonly IOrderingRepository _orderingRepository;
        public CreateOrderCommandHandler(IOrderingRepository orderingRepository)
        {
            _orderingRepository = orderingRepository;
        }
        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderingRepository.CreateAsync(new Ordering()
            {
                UserId = request.UserId,
                TotalPrice = request.TotalPrice,
                OrderDetails = request.OrderDetails.Select(d => new OrderDetail
                {
                    ProductId = d.ProductId,
                    ProductName = d.ProductName,
                    ProductPrice = d.ProductPrice,
                    ProductAmount = d.ProductAmount,
                    ProductTotalPrice = d.TotalPrice
                }).ToList()
            });

            return new CreateOrderCommandResponse
            {
                Message = "order başarıyla oluşturuldu"
            };
        }
    }
}
