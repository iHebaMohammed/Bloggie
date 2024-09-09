using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Demo.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account account;
        public CloudinaryImageRepository(IConfiguration configuration)
        {
            _configuration=configuration;
            account = new Account(
                    configuration.GetSection("Cloudinary")["CloudName"],
                    configuration.GetSection("Cloudinary")["ApiKey"],
                    configuration.GetSection("Cloudinary")["ApiSecret"]
                );
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var clinet = new Cloudinary(account);

            var fileName = $"{Guid.NewGuid}{file.FileName}";
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, file.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
                DisplayName = file.FileName
            };
            var uploadResult = await clinet.UploadAsync(uploadParams);
            if(uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            return null;
        }
    }
}
