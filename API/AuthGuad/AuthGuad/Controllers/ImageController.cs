using AuthGuad.Helper;
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
        public async Task<IActionResult> UploadImage(IFormFile formFile, string icode)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                string FilePath = GetFilePath(icode);

                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }

                string imagePath = FilePath + "\\" + icode + ".png";
                if (System.IO.Directory.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);

                }
                using (FileStream stream = System.IO.File.Create(imagePath))
                {
                    await formFile.CopyToAsync(stream);
                    response.ResponseCode = 200;
                    response.Result = "pass";
                }


            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost("MultiUploadImage")]
        public async Task<IActionResult> MultiUploadImage(IFormFileCollection formcollection, string icode)
        {
           ApiResponse response = new ApiResponse();
            int passcount = 0; int errorcount =0;
            try
            {
                string FilePath = GetFilePath(icode);

                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }
                foreach (var file in formcollection)
                {

                    string imagePath = FilePath + "\\" + file.FileName;

                    if (System.IO.Directory.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);

                    }
                    using (FileStream stream = System.IO.File.Create(imagePath))
                    {
                        await file.CopyToAsync(stream);
                        passcount++;
                       
                    }
                }
               

            }
            catch(Exception ex)
            {
               errorcount++;
               response.ErrorMessage = ex.Message;
            }
            response.ResponseCode = 200;
            response.Result = passcount + "File Uploaded & " + errorcount + " file file";
            return Ok(response);
        }


        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(string icode)
        {
            string ImageUrl = string.Empty;
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string FilePath = GetFilePath(icode);
                string imagePath = FilePath + "\\" + icode + ".png";
                if(System.IO.File.Exists(imagePath))
                {
                    ImageUrl = hostUrl + "/Upload/Image/"+icode+"/"+icode+".png";
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {

            }
            return Ok(ImageUrl);
        } 
        
        [HttpGet("GetMultiImage")]
        public async Task<IActionResult> GetMultiImage(string icode)
        {
            List<string> ImageUrl = new List<string>();
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string FilePath = GetFilePath(icode);
                if (System.IO.Directory.Exists(FilePath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(FilePath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                       string filename = fileInfo.Name;
                       string imagePath = FilePath + "\\" + filename;
                        if (System.IO.File.Exists(imagePath))
                        {
                           string _ImageUrl = hostUrl + "/Upload/Image/" + icode + "/" + filename;
                            ImageUrl.Add(_ImageUrl);
                        }
                    }
                  

                }

               
                
                
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {

            }
            return Ok(ImageUrl);
        }
        [NonAction]
       private string GetFilePath(string icode)
        {
            return this.environment.WebRootPath + "\\Upload\\Image\\" + icode;
        }
    }
}
