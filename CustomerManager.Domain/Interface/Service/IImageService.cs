using Microsoft.AspNetCore.Http;

namespace CustomerManager.Domain.Interface.Service
{
    public interface IImageService
    {
        string ConvertIFormFileToBase64(IFormFile file);
    }
}
