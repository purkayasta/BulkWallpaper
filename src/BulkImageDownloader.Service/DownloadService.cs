namespace BulkImageDownloader.Service
{
    public class DownloadService
    {
        private readonly HttpClient _httpClient;
        public DownloadService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task SaveAsync(HttpResponseMessage response, string localPathWithImageName)
        {
            using var fileStream = await response.Content.ReadAsStreamAsync();
            using var streamWriter = File.Open(localPathWithImageName, FileMode.Create);
            await fileStream.CopyToAsync(streamWriter);
        }
        public async Task<HttpResponseMessage> GetContentAsync(string url)
            => await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
    }
}
