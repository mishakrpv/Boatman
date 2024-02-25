using Boatman.Utils.Models.Response;
using Microsoft.AspNetCore.Http;

namespace Boatman.BlobStorage.Interfaces;

public interface IBlobStorage
{
    Task<Response<string>> UploadAsync(IFormFile file);
}