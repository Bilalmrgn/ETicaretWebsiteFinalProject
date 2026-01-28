
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Queries.Address.GetAllAdress;
using Order.Application.Features.Queries.Address.GetByIdAddress;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        public readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses(CancellationToken cancellationToken)
        {
            var query = new GetAllAdressQueryRequest();

            var response = await _mediator.Send(query, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAddresses(int id, CancellationToken cancellationToken)
        {
            var address = new GetByIdAddressQueryRequest(id);
            
            var resposne = await _mediator.Send(address, cancellationToken);

            return Ok(resposne);
        }
    }
}
