using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Commands.Addresses.DeleteAddress;
using Order.Application.Features.Commands.Orders.CreateOrder;
using Order.Application.Features.Commands.Orders.DeleteOrder;
using Order.Application.Features.Commands.Orders.UpdateOrder;
using Order.Application.Features.Queries.Order.GetAllOrder;
using Order.Application.Features.Queries.Order.GetByIdOrder;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
        {
            var order = new GetAllOrderQueryRequest();

            var response = await _mediator.Send(order, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id, CancellationToken cancellationToken)
        {
            var order = new GetByIdOrderQueryRequest(id);

            var response = await _mediator.Send(order, cancellationToken);

            return Ok(response);
        }

        //order create
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            CreateOrderCommandResponse response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        //order update
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            request.OrderingId = id;

            UpdateOrderCommandResponse response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        //order delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id,DeleteOrderCommandRequest request ,CancellationToken cancellationToken)
        {
            request.OrderId = id;

            await _mediator.Send(request,cancellationToken);

            return NoContent();
        }
    }
}
