using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Boatman.BlobStorage.Interfaces;
using Boatman.Logging.Interfaces;
using Boatman.Utils.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Boatman.BlobStorage.Implementations.AmazonS3;

public class AmazonS3BlobStorage : IBlobStorage
{
    private const string BucketName = "boatman";

    private readonly AWSCredentials _credentials;
    private readonly AmazonS3Config _configsS3 = new AmazonS3Config()
    {
        ServiceURL = "https://s3.yandexcloud.net"
    };

    private readonly IAppLogger<AmazonS3BlobStorage> _logger;

    public AmazonS3BlobStorage(IOptions<AwsCredentials> options,
        IAppLogger<AmazonS3BlobStorage> logger)
    {
        var credentials = options.Value;
        _credentials = new BasicAWSCredentials(credentials.AwsKey, credentials.AwsSecretKey);
        _logger = logger;
    }

    public async Task<Response<string>> UploadAsync(IFormFile file)
    {
        var key = $"{Guid.NewGuid().ToString()}{file.FileName}";
        
        var response = await UploadToS3Async(file, key);

        if (response.StatusCode == 200)
        {
            _logger.LogInfo(response.Message);

            return response;
        }
        
        _logger.LogWarn(response.Message);

        return response;
    }

    private async Task<Response<string>> UploadToS3Async(IFormFile file, string key)
    {
        var response = new Response<string>();

        using var s3Client = new AmazonS3Client(_credentials, _configsS3);
        await using (var inputStream = file.OpenReadStream())
        {
            var putRequest = new PutObjectRequest()
            {
                InputStream = inputStream,
                Key = key,
                BucketName = BucketName,
                ContentType = file.ContentType,
                CannedACL = S3CannedACL.PublicRead
            };
                
            try
            {
                await s3Client.PutObjectAsync(putRequest);

                response.Value = $"https://storage.yandexcloud.net/{BucketName}/{key}";
                response.StatusCode = 200;
                response.Message = $"{key} object has been uploaded successfully";
            }
            catch (AmazonS3Exception ex)
            {
                response.StatusCode = (int)ex.StatusCode;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }
                
            return response;
        }
    }
}