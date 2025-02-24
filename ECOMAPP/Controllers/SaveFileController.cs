using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;

namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SaveFileController : Controller
    {

        [Route("SaveImage")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin,Roles.User,Roles.Vendor])]
        public async Task<IActionResult> SaveImage(IFormFile Image, [FromForm] string folderType)
        {

            try
            {
                if (string.IsNullOrEmpty(folderType))
                {
                    return BadRequest(new { Code = 401, message = "Folder type is required.", success = false });
                }

                if (Image == null || Image.Length <= 0)
                {
                    return BadRequest(new { Code = 401, message = "Invalid or empty file provided.", success = false });
                }

                string baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Content");
                string folderPath = Path.Combine(baseFolder, folderType.ToLower());

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileExtension = Path.GetExtension(Image.FileName);
                string uniqueFileName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}{fileExtension}";
                string filePath = Path.Combine(folderPath, uniqueFileName);

                // Save the image
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                string relativePath = Path.Combine("Content", folderType.ToLower(), uniqueFileName).Replace("\\", "/");
                string fileUrl = $"{Request.Scheme}://{Request.Host}/{relativePath}";

                return Ok(new
                {
                    Code = 200,
                    message = "Image saved successfully.",
                    success = true,
                    path = $"/{relativePath}"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Code = 500,
                    message = $"An error occurred: {ex.Message}",
                    Retval = "Failed"
                });
            }
        }

        //[JwtAuthorization(Roles = new[] { Roles.Admin, Roles.Vendor, Roles.User })]
        //public JsonResult SaveImage()
        //{
        //    try
        //    {
        //        string folderType = Request.Form["folderType"];

        //        if (string.IsNullOrEmpty(folderType))
        //        {
        //            return Json(new { Code = 401, message = "Folder type is required.", success = false });
        //        }

        //        var imageFile = Request.Files["Image"];

        //        if (imageFile == null || imageFile.ContentLength <= 0)
        //        {
        //            return Json(new { Code = 401, message = "Invalid or empty file provided.", success = false });
        //        }


        //        string baseFolder = Server.MapPath("~/Content");
        //        string folderPath = Path.Combine(baseFolder, folderType.ToLower());

        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }


        //        string fileExtension = Path.GetExtension(imageFile.FileName);
        //        string uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmssfff}{fileExtension}";
        //        string filePath = Path.Combine(folderPath, uniqueFileName);

        //        // Save the image
        //        imageFile.SaveAs(filePath);


        //        string relativePath = Url.Content($"~/Content/{folderType.ToLower()}/{uniqueFileName}");

        //        return Json(new
        //        {
        //            Code = 200,
        //            message = "Image saved successfully.",
        //            success = true,
        //            path = relativePath
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new
        //        {
        //            Code = 500,
        //            message = $"An error occurred: {ex.Message}",
        //            Retval = "Failed"
        //        });
        //    }




        //}





    }
}
