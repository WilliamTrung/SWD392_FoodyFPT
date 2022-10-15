using Microsoft.AspNetCore.Http;
using Service.DTO;

namespace FoodyAPI.Helper.Azure.IBlob
{
    public interface IProductBlob
    {
        public Task<bool> UploadAsync(List<IFormFile> files, int productId);
        public List<string>? GetURL(Product product);
    }
}
