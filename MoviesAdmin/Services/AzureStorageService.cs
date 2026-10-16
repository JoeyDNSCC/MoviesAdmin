using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace MoviesAdmin.Services
{

    public class AzureStorageService : IAzureStorageService
    {
        private readonly IConfiguration _configuration;

        private readonly BlobServiceClient blobServiceClient;
        private string containerName;
        
        // constructor
        public AzureStorageService(IConfiguration configuration)
        {
            // config used to get appsettings, user-secrets, environment vars, etc
            _configuration = configuration;

            // get Azure container name & connection string from appsettings.json
            containerName = _configuration["AzureStorage:ContainerName"] ?? throw new InvalidOperationException("Missing configuration: AzureStorage:ContainerName");
            string connectionString = _configuration["AzureStorage:ConnectionString"] ?? throw new InvalidOperationException("Missing configuration: AzureStorage:ConnectionString");

            // init Azure blob client
            blobServiceClient = new BlobServiceClient(connectionString);
        }


        // func from interface file
        public async Task<string> UploadFileAsync(IFormFile file, string fileName)
        {
            // get client for containerName (e.g. "movies" or "uploads")
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(fileName);

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                }
            };

            // file i/o
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, options); // pass in options when uploading
            }

            // return url for the savedfile
            return blobClient.Uri.ToString();
        }
    }

}