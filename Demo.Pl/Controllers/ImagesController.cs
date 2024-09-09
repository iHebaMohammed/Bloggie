using Demo.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Demo.Pl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository=imageRepository;
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return Ok("This is the get images api call");
        //}

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            // call the repository
            var imageUrl = await imageRepository.UploadAsync(file);
            if (imageUrl == null) 
            {
                return Problem("Somethingwent wrong!" , null , (int) HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageUrl});
        }

    }
}
