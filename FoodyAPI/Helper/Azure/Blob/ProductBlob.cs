using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.DTO;
using FoodyAPI.Helper.Azure.IBlob;
using Service.Services.IService;

namespace FoodyAPI.Helper.Azure.Blob
{
    public class ProductBlob : IProductBlob
    {
        IConfiguration _config;
        IProductService _productService;

        BlobServiceClient? blobStorage;
        BlobContainerClient? blobContainer;

        private string container;
        public ProductBlob(IConfiguration config, IProductService productService)
        {
            _config = config;
            _productService = productService;
            blobStorage = AzureService.GetBlobServiceClient(_config);

            var blobStorage_config = _config.GetSection("BlobStorage");
            container = blobStorage_config["ProductContainer"];

            blobContainer = AzureService.CheckBlobContainerAsync(blobStorage, container).Result;//blobStorage.GetBlobContainerClient(container);
        }

        public List<string>? GetURL(Product product)
        {
            //throw new NotImplementedException();
            List<string>? result = null;
            if (blobStorage == null || blobContainer == null)
                return result;
            string blobName = product.Id.ToString() + ".png";
            string prefix = product.Id.ToString() + "_";
            var blobs = blobContainer.GetBlobs(prefix: prefix);
            string url_prefix = blobContainer.Uri.ToString();
            result = new List<string>();
            foreach(var blob in blobs)
            {
                string url = url_prefix +"/"+ blob.Name;
                result.Add(url);
            }
            //result = blob.Uri.ToString();
            return result;
        }
        public async Task<bool> UploadAsync(List<IFormFile> files, int productId)
        {
            //throw new NotImplementedException();
            try
            {
                if (blobStorage == null)
                    return false;
                var product = (await _productService.GetAsync(filter: p => p.Id == productId)).FirstOrDefault();
                if (product == null)
                    return false;
                if (blobContainer != null)
                {
                    int i = 0;
                    string prefix = product.Id.ToString() + "_";
                    foreach (var file in files)
                    {
                        string extension = file.ContentType.Split("/")[1];
                        string filename = productId.ToString() + "_"+ (++i) + "." + extension;
                        var blob = blobContainer.GetBlobClient(filename);
                        var stream = file.OpenReadStream();
                        await blob.UploadAsync(stream, overwrite: true);
                        
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            } catch
            {
                return false;
            }

        }

    }
}
