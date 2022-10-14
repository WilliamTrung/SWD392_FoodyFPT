using Microsoft.AspNetCore.Http;
using Service.DTO;

namespace FoodyAPI.Helper.Azure.IBlob
{
    public interface IProductBlob
    {
        public Task<bool> UploadAsync(IFormFile file, int productId);
        public string? GetURL(Product product);
    }
}
