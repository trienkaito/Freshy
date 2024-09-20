using CloudinaryDotNet;
using FRESHY.SharedKernel.Interfaces;
using FRESHY.SharedKernel.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;

namespace FRESHY.SharedKernel.Persistance.Repositories;

public class ImageService : IImageService
{
    private readonly Account account;
    private readonly CloudinarySettings _cloudinarySettings;

    public ImageService(IOptions<CloudinarySettings> options)
    {
        _cloudinarySettings = options.Value;
        account = new Account(
            _cloudinarySettings.CloudName,
            _cloudinarySettings.ApiKey,
            _cloudinarySettings.SecretKey);
    }

    public async Task<string?> UploadAsync(IFormFile file)
    {
        var client = new Cloudinary(account);
        var uploadFileResult = await client.UploadAsync(
            new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            });

        if (uploadFileResult != null && uploadFileResult.StatusCode == HttpStatusCode.OK)
        {
            return uploadFileResult.SecureUrl.ToString();
        }
        return null;
    }
}