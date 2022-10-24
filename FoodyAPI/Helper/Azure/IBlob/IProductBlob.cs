using Microsoft.AspNetCore.Http;
using Service.DTO;

namespace FoodyAPI.Helper.Azure.IBlob
{
    public interface IProductBlob : IBlobService
    {
        public Task<bool> UploadAsync(List<IFormFile> files, int id);
        public List<string>? GetURLs(int id);
    }
}
