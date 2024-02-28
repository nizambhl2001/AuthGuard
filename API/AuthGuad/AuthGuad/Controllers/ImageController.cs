using AuthGuad.Data;
using AuthGuad.Helper;
using AuthGuad.Models;
using AuthGuad.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

namespace AuthGuad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicaitonDbContext dbContext;
        private readonly IcustomerService icustomer;

        public ImageController(IWebHostEnvironment environment, ApplicaitonDbContext dbContext, IcustomerService icustomer)
        {
            this.environment = environment;
            this.dbContext = dbContext;
            this.icustomer = icustomer;
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
        
        [HttpGet("download")]
        public async Task<IActionResult> download(string icode)
        {
           
            try
            {
                string FilePath = GetFilePath(icode);
                string imagePath = FilePath + "\\" + icode + ".png";
                if (System.IO.File.Exists(imagePath))
                {
                    MemoryStream stream = new MemoryStream();
                    using (FileStream fileStream = new FileStream(imagePath, FileMode.Open))
                    {
                        await fileStream.CopyToAsync(stream);
                    }
                    stream.Position = 0;
                    return File(stream, "image/png",icode + ".png");

                }

                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return NotFound();
            }
           
        } 
        
        [HttpGet("RemoveImage")]
        public async Task<IActionResult> RemoveImage(string icode)
        {
           
            try
            {
                string FilePath = GetFilePath(icode);
                string imagePath = FilePath + "\\" + icode + ".png";
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                    return Ok("pass");

                }

                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return NotFound();
            }
           
        } 
        
        [HttpGet("MultiRemoveImage")]
        public async Task<IActionResult> MultiRemoveImage(string icode)
        {
           
            try
            {
                string FilePath = GetFilePath(icode);
                if (System.IO.Directory.Exists(FilePath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(FilePath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        fileInfo.Delete();
                    }
                    return Ok("pass");
                }

                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return NotFound();
            }
           
        }


        [HttpPost("DbMultiUploadImage")]
        public async Task<IActionResult> DbMultiUploadImage(IFormFileCollection formcollection, string icode)
        {
            ApiResponse response = new ApiResponse();
            int passcount = 0; int errorcount = 0;
            try
            {
               
                foreach (var file in formcollection)
                {

                    using (MemoryStream stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        this.dbContext.products.Add(new ProductImage()
                        {
                            PCode = icode,
                            PImage = stream.ToArray(),
                        });

                        await this.dbContext.SaveChangesAsync();
                        passcount++;
                    }

                   
                }


            }
            catch (Exception ex)
            {
                errorcount++;
                response.ErrorMessage = ex.Message;
            }
            response.ResponseCode = 200;
            response.Result = passcount + "File Uploaded & " + errorcount + " file file";
            return Ok(response);
        }

        [HttpGet("GetDbMultImage")]
        public async Task<IActionResult> GetDbMultImage(string icode)
        {
            List<string> ImageUrl = new List<string>();
            try
            {
                var productIamge = this.dbContext.products.Where(item => item.PCode == icode).ToList();
                if (productIamge != null && productIamge.Count >0)
                {
                    productIamge.ForEach(item =>
                    {
                        ImageUrl.Add(Convert.ToBase64String(item.PImage));
                    });
                   
                }

                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(ImageUrl);
        }

        [HttpGet("GetDbSingleImage")]
        public async Task<IActionResult> GetDbSingleImage(string icode)
        {
            var i = await this.icustomer.GetDbSingleImage(icode);
            return Ok(i);
        }


        [NonAction]
       private string GetFilePath(string icode)
        {
            return this.environment.WebRootPath + "\\Upload\\Image\\" + icode;
        }
    }
}
