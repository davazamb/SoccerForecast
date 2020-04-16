using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerForecast.Web.Helpers
{
    public interface IImageHelper
    {
        string UploadImage(byte[] pictureArray, string folder);
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }

}
