using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly ISender _mediator;


        public IActionResult Index()
        {
            return View();
        }
    }
}
