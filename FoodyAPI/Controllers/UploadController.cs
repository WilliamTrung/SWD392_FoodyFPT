using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using System.Net;
using NuGet.Protocol;
using Service.Helper;

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
            //for testing
            var blobStorage = _config.GetSection("BlobStorage");
            var containerName = blobStorage["ContainerName"]; 
            //var imgSrc = _config.GetSection("Images");
            //var imgStorage = imgSrc["Storage"];

            Stream myBlob =  file.OpenReadStream();
            var blobContainer = AzureService.GetBlobContainer(_config, containerName);
            var blob = blobContainer.GetBlobClient(file.FileName);
            await blob.UploadAsync(myBlob);
            return Ok(AzureService.GetBlobResourcePath(_config, containerName, file.FileName));

        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
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
        }
    }
}
