using Microsoft.AspNetCore.Http;

namespace FRESHY.SharedKernel.Interfaces;

public interface IImageService
{
    Task<string?> UploadAsync(IFormFile file);
}