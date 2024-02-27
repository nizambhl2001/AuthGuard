using AuthGuad.Data;
using AuthGuad.Dto;
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
        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var cusmoter = await service.GetCustomer();
            if (cusmoter == null)
            {
                return NotFound();
            }
            return Ok(cusmoter);
        }
        [HttpGet("GetByCode")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var cusmoter = await service.GetByCode(code);
            if (cusmoter == null)
            {
                return NotFound();
            }
            return Ok(cusmoter);
        }
       
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CustomerDto data)
        {

            var cusmoter = await service.Create( data);
            if (cusmoter == null)
            {
                return NotFound();
            }
            return Ok(cusmoter);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(CustomerDto data, string code)
        {

            var cusmoter = await service.Update( data,code);
            if (cusmoter == null)
            {
                return NotFound();
            }
            return Ok(cusmoter);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(string code)
        {
            var cusmoter = await service.Remove(code);
            if (cusmoter == null)
            {
                return NotFound();
            }
            return Ok(cusmoter);
        }
    }
}
