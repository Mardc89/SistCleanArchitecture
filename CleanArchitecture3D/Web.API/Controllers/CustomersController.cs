using Application.Abstractions;
using Application.Customers.Create;
using Domain.Customers;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("Customers")]
    public class CustomersController : ApiController
    {
        private readonly ISender _mediator;

        public CustomersController(ISender mediator)
        {
            _mediator = mediator??throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task <IActionResult> Create([FromBody]CreateCustomerCommand command)
        {
            var createCustomerResult = await _mediator.SendAsync(command);

            return createCustomerResult.Match(
                customer=>Ok(),
                errors=>Problem(errors)
            );
        }
    }
}
