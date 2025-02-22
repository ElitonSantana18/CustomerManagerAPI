using CustomerManager.Domain.Interface.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManager.Service.Service
{
    public class ImageService : IImageService
    {
        public string ConvertIFormFileToBase64(IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            return Convert.ToBase64String(ms.ToArray());
        }
    }

}
