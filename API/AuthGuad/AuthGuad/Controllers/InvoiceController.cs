using AuthGuad.Dto;
using AuthGuad.Services;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;


namespace AuthGuad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _invoice;
        private readonly IWebHostEnvironment environment;

        public InvoiceController(IInvoiceServices invoice, IWebHostEnvironment environment)
        {
            _invoice = invoice;
            this.environment = environment;
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
        [HttpGet("GenPDFwithImage")]
        public async Task<IActionResult> GenPDFwithImage()
        {
            var document = new PdfDocument();
            string htmlelement = "<div style='width:100%'>";
            // string imgeurl = "https://res.cloudinary.com/demo/image/upload/v1312461204/sample.jpg";
            //string imgeurl = "https://" + HttpContext.Request.Host.Value + "/Uploads/common/logo.jpeg";
            string imgeurl = "data:image/png;base64, " + Getbase64string() + "";
            htmlelement += "<img style='width:80px;height:80%' src='" + imgeurl + "'   />";
            htmlelement += "<h2>Welcome to <br> Eng. MD NIZAM UDDIN TUHIN</h2>";
            htmlelement += "<h2>Software Arena Limited</h2>";
            htmlelement += "</div>";
            PdfGenerator.AddPdfPages(document, htmlelement, PageSize.A4);
    
            byte[] response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            return File(response, "application/pdf", "PDFwithImage.pdf");

        }

        [NonAction]
        public string Getbase64string()
        {
            string filepath = this.environment.WebRootPath + "\\Upload\\common\\logo.jpg";
            byte[] imgarray = System.IO.File.ReadAllBytes(filepath);
            string base64 = Convert.ToBase64String(imgarray);
            return base64;
        }
    }
}
