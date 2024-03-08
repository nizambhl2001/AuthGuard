using AuthGuad.Data;
using AuthGuad.Dto;
using AuthGuad.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthGuad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService container;
        private readonly IWebHostEnvironment _environment;
        public ProductsController(IProductService container, IWebHostEnvironment environment)
        {
            this.container = container;
            this._environment = environment;
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
