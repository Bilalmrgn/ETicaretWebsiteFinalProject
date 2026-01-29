using MediatR;
using Order.Application.Features.Commands.Orders.Dto;
using Order.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Commands.Orders.CreateOrder
{
    public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
    {
        public string UserId { get; set; }
        public List<OrderDetailDtos> OrderDetails { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
