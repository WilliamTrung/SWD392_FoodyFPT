using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Net;
using NuGet.Protocol;
using FoodyAPI.Helper.Azure;
using FoodyAPI.Helper.Azure.IBlob;

namespace FoodyAPI.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : Controller
    {
        IConfiguration _config;
        IProductBlob _productBlob;
        public UploadController(IConfiguration configuration, IProductBlob productBlob)
        {
            _config = configuration;
            _productBlob = productBlob;
        }
        [HttpPost("product")]
        public async Task<IActionResult> Post(List<IFormFile> pictures, int productId)
        {
            foreach (var picture in pictures)
            {
                if (!picture.ContentType.Contains("image"))
                {
                    return BadRequest();
                }
            }
            var check = await _productBlob.UploadAsync(pictures, productId);
            if (!check)
            {
                return Ok(StatusCodes.Status500InternalServerError);
            }
            return Ok(StatusCodes.Status200OK);
        }
    }
}
