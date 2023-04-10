namespace BulkWallpaper.Service
{
    public class DownloadService
    {
        private readonly HttpClient _httpClient;
        public DownloadService(HttpClient httpClient) => _httpClient = httpClient;

        public void Download(string url, string localPath, string imageName, string imageExtension)
        {
            try
            {
                var physicalPathInfo = Directory.CreateDirectory(localPath);
                if (!physicalPathInfo.Exists)
                    throw new Exception("Invalid Directory");

                var downloadablePath = physicalPathInfo.FullName + "/" + imageName + "." + imageExtension;

                using var stream = _httpClient.GetStreamAsync(url).GetAwaiter().GetResult();
                using var fileStream = File.Create(downloadablePath);
                stream.CopyTo(fileStream);

                Console.WriteLine($"\n File downloaded at {downloadablePath} ✅");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
