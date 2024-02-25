using AuthGuad.Data;
using AuthGuad.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthGuad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IcustomerService service;

        public CustomersController(IcustomerService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var cusmoter = await service.Customer();
            if (cusmoter == null)
            {
                return NotFound();
            }
            return Ok(cusmoter);
        }
    }
}
