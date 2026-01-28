using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Queries.Order.GetByIdOrder
{
    public class GetByIdOrderQueryRequest : IRequest<GetByIdOrderQueryResponse>
    {
        public int OrderId { get; set; }
        public GetByIdOrderQueryRequest(int orderId)
        {
            OrderId = orderId;
        }
    }
}
