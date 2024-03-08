using AuthGuad.Data;
using AuthGuad.Dto;
using AuthGuad.Helper;
using AuthGuad.Models;
using AuthGuad.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthGuad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService container;
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicaitonDbContext context;

        public ProductsController(IProductService container, IWebHostEnvironment environment,ApplicaitonDbContext context)
        {
            this.container = container;
            this._environment = environment;
            this.context = context;
        }
        [HttpPost("SaveProductWtihImageDatabase")]
 
        public async Task<IActionResult> CreateProduct([FromForm] TblProduct product)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                // Ensure the product has a valid code
                if (string.IsNullOrEmpty(product.PCode))
                {
                    response.ErrorMessage = "Product code is required.";
                    return BadRequest(response);
                }

                // Create directory if it doesn't exist
                string FilePath = GetFilePath(product.PCode);
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }

                // Construct image path and save image
                string imagePath = Path.Combine(FilePath, product.PCode + ".png");
                using (FileStream stream = System.IO.File.Create(imagePath))
                {
                    await product.formFile.CopyToAsync(stream);
                    response.ResponseCode = 200;
                    response.Result = "pass";
                }

                // Set the product's image path
                product.PImage = imagePath;

                // Save the product to the database
                this.context.tblproducts.Add(product);
                await this.context.SaveChangesAsync();

                // Generate and save the image URL
                product.PImage = GenerateImageUrl(product.PCode);

                // Update the product in the database with the image URL
                await this.context.SaveChangesAsync();

                // Return the newly created product
                return CreatedAtAction(nameof(GetProduct), new { PCode = product.PCode }, product);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                return StatusCode(500, response);
            }
        }

        
        private string GenerateImageUrl(string icode)
        {
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            return $"{hostUrl}/Upload/Image/{icode}/{icode}.png";
        }
    

    [HttpGet("GetProduct")]
        public async Task<ProductEntity> GetProduct(string Code)
        {
            var product = await this.container.Getbycode(Code);
         
            return product;

        }




        [HttpGet("GetAll")]
        public async Task<List<ProductEntity>> GetAll()
        {
            var productlist = await this.container.Getall();
            if (productlist != null && productlist.Count > 0)
            {
                productlist.ForEach(item =>
                {
                    item.productImage = GetImagebyProduct(item.PCode);
                });
            }
            else
            {
                return new List<ProductEntity>();
            }
            return productlist;

        }
        [HttpGet("GetByCode")]
        public async Task<ProductEntity> GetByCode(string Code)
        {
            var product = await this.container.Getbycode(Code);
            if (product != null)
            {
                product.productImage = GetImagebyProduct(Code);
            }
            else
            {
                return new ProductEntity();
            }
            return product;
         

        }

        [NonAction]
        private string GetFilePath(string ProductCode)
        {
            return this._environment.WebRootPath + "\\Upload\\Image\\" + ProductCode;
        }
        [NonAction]
        private string GetImagebyProduct(string icode)
        {
            string ImageUrl = string.Empty;
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
           
                string FilePath = GetFilePath(icode);
                string imagePath = FilePath + "\\" + icode + ".png";
                if (System.IO.File.Exists(imagePath))
                {
                    ImageUrl = hostUrl + "/Upload/Image/" + icode + "/" + icode + ".png";
                }
                else
                {
                    ImageUrl = hostUrl + "/upload/common/noimage.png";
                }
                return ImageUrl;
            
        }
    }
  
}
