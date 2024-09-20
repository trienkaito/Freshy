using FRESHY.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FRESHY_API.Controllers
{
    [ApiController]
    [Route("image")]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imgUrl = await _imageService.UploadAsync(file);
            if (imgUrl == null)
            {
                return BadRequest("Something went wrong...");
            }
            return Json(new { link = imgUrl });
        }
    }
}
