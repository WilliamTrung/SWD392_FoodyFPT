using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Net;
using NuGet.Protocol;
using FoodyAPI.Helper.Azure;

namespace FoodyAPI.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : Controller
    {
        IConfiguration _config;
        public UploadController(IConfiguration configuration)
        {
            _config = configuration;
        }
        private async Task<IActionResult> Upload(IFormFile file)
        {
            /*
            //for testing
            var blobStorage = _config.GetSection("BlobStorage");
            var containerName = blobStorage["ContainerName"]; 
            //var imgSrc = _config.GetSection("Images");
            //var imgStorage = imgSrc["Storage"];

            Stream myBlob =  file.OpenReadStream();
            //set blob container
            var blobContainer = AzureService.CheckBlobContainer(_config, containerName);
            if(blobContainer == null)
            {
                return Ok(StatusCodes.Status404NotFound);
            }
            //set file
            var blob = blobContainer.GetBlobClient(file.FileName);
            //upload file
            await blob.UploadAsync(myBlob);
            return Ok(AzureService.GetBlobResourcePath(_config, containerName, file.FileName));
            */
            return BadRequest(StatusCodes.Status503ServiceUnavailable);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            /*
            if(files.Count == 0)
            {
                return BadRequest();
            } else
            {
                string check = "";
                foreach(var file in files)
                {
                    check += await Upload(file);
                }
                return Ok(check);
            }
            */
            return BadRequest(StatusCodes.Status503ServiceUnavailable);
        }
    }
}
