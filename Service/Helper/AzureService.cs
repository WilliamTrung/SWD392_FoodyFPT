using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helper
{
    public class AzureService
    {
        public static BlobServiceClient GetBlobServiceClient(IConfiguration _config)
        {
            try
            {
                var blobStorage = _config.GetSection("BlobStorage");
                var connection = blobStorage["AzureWebJobsStorage"];
                BlobServiceClient blob = new BlobServiceClient(connection);
                return blob;
            }  
            catch
            {
                throw new Exception("Cannot retrieve blob storage data!");
            }
        }
        public static BlobContainerClient GetBlobContainer(IConfiguration _config, string containerName)
        {
            try
            {
                var blobStorage = GetBlobServiceClient(_config);
                var blobContainer = blobStorage.GetBlobContainerClient(containerName);
                return blobContainer;
            } catch
            {
                throw new Exception("Cannot retrieve blob container!");
            }
        }
        public static bool CreateBlobContainer(IConfiguration _config, string containerName)
        {
            var blobStorage = GetBlobServiceClient(_config);
            if(blobStorage != null)
            {
                try
                {
                    blobStorage.CreateBlobContainerAsync(containerName).Wait();
                    return true;
                }
                catch
                {
                    throw new Exception("Cannot create blob container due to Azure services!");
                }
                
            } else
            {
                return false;
            }
        }
        public static string GetBlobResourcePath(IConfiguration _config, string containerName, string fileName)
        {
            string path = _config.GetSection("Images")["Storage"];
            path = Path.Combine(path, containerName, fileName);
            return path;
        }
    }
}
