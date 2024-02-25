using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AuthGuad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public ImageController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage()
        {
            bool result = false;
          
            try
            {
                var _uploadedfile = Request.Form.Files;
                foreach (IFormFile source in _uploadedfile)
                {
                    string UserName = source.FileName;
                    string FilePath = GetFilePath(UserName);

                    if (!System.IO.Directory.Exists(FilePath)) 
                    {
                        System.IO.Directory.CreateDirectory(FilePath);  
                    }

                    string imagePath = FilePath + "\\image.png";
                    if (System.IO.Directory.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);

                    }
                    using (FileStream stream = System.IO.File.Create(imagePath))
                    {
                        await source.CopyToAsync(stream);
                        result = true;
                    }
                }

            }
            catch(Exception ex)
            {
               
            }
            return Ok(result);
        }
        [NonAction]
       private string GetFilePath(string UserName)
        {
            return this.environment.WebRootPath + "\\images" + UserName;
        }
    }
}
