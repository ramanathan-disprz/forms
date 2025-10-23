using Amazon.S3;
using Amazon.S3.Model;

namespace forms.AWS;

public class AmazonS3Helper
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public AmazonS3Helper(IConfiguration configuration)
    {
        var awsConfig = configuration.GetSection("AWS");
        _bucketName = awsConfig["BucketName"] ?? "";
        _s3Client = new AmazonS3Client(
            awsConfig["AccessKey"],
            awsConfig["SecretKey"],
            Amazon.RegionEndpoint.GetBySystemName(awsConfig["Region"])
        );
    }

    public string GeneratePreSignedUploadUrl(string key, string contentType, int expiryMinutes = 20)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = key,
            Verb = HttpVerb.PUT, // For uploading
            ContentType = contentType,
            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes)
        };

        return _s3Client.GetPreSignedURL(request);
    }

    public string GeneratePreSignedDownloadUrl(string key, int expiryMinutes = 20)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = key,
            Verb = HttpVerb.GET, // For downloading
            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes)
        };

        return _s3Client.GetPreSignedURL(request);
    }
}