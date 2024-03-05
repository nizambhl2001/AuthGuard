using AuthGuad.Dto;
using AuthGuad.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _invoice;

        public InvoiceController(IInvoiceServices invoice)
        {
            _invoice = invoice;
        }
        [HttpGet("GetAllInoviceHeaders")]
        public async Task<List<InoviceHeader>> GetAllInoviceHeaders()
        {
            return await _invoice.GetAllInoviceHeadersAsync();
           
        }
        
        [HttpGet("GetInoviceHeadersByCode")]
        public async Task<InoviceHeader> GetInoviceHeadersByCode(string invoiceNo)
        {
            return await _invoice.GetInoviceHeadersByCodeAsync(invoiceNo);
           
        }

        [HttpGet("GetAllInoviceDetials")]
        public async Task<List<InvoiceDetials>> GetAllInoviceDetials()
        {
            return await _invoice.GetInoviceDetailsAsync();

        }

        [HttpGet("GetInoviceDetailsByCode")]
        public async Task<InvoiceDetials> GetInoviceDetailsByCode(string invoiceNo)
        {
            return await _invoice.GetInoviceDetailsByCodeAsync(invoiceNo);

        }
    }
}
