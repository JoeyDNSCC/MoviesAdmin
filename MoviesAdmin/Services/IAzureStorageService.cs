namespace MoviesAdmin.Services
{
    public interface IAzureStorageService
    {
        // service to upload file to azure blob storage
        // return: the url of the uploaded file as a string
        public Task<string> UploadFileAsync(IFormFile file, string fileName);

        public Task<bool> DeleteFileAsync(string fileName);
    }
}
