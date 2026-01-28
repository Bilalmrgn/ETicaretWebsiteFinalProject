using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Queries.Address.GetAllAdress;
using Order.Application.Features.Queries.Address.GetByIdAddress;

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
            var order = new GetAllAdressQueryRequest();

            var response = await _mediator.Send(order, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id,CancellationToken cancellationToken)
        {
            var order = new GetByIdAddressQueryRequest(id);

            var response  = await _mediator.Send(order,cancellationToken);

            return Ok(response);
        }
    }
}
