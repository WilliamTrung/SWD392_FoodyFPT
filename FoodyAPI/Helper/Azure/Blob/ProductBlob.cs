using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.DTO;
using FoodyAPI.Helper.Azure.IBlob;
using Service.Services.IService;

namespace FoodyAPI.Helper.Azure.Blob
{
    public class ProductBlob : Blob, IProductBlob 
    {
        IProductService _productService;
        public string Container { get; private set; }
        public ProductBlob(IConfiguration config, IProductService productService) : base(config)
        {
            _productService = productService;
            Container = config.GetSection("BlobStorage")["ProductContainer"];
        }
        public List<string>? GetURLs(int id)
        {
            return this.GetURLs(Container, id);
        }
        public async Task<bool> UploadAsync(List<IFormFile> files, int id)
        {
            //validate
            var find = await _productService.GetAsync(filter: p => p.Id == id);
            if(find == null)
            {
                return false;
            }
            return base.UploadAsync(files, Container, id).Result;
        }
    }
}
