using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli.Services
{
    public abstract class BaseService
    {
        private readonly HttpClient _httpClient;
        public BaseService(IHttpClientFactory httpClientFactory, ClientEnum wallpaperClient)
        {
            _httpClient = httpClientFactory.CreateClient(wallpaperClient.ToString());
        }
        public async Task SaveAsync(HttpResponseMessage response, string localPathWithImageName)
        {
            using var fileStream = await response.Content.ReadAsStreamAsync();
            using var streamWriter = File.Open(localPathWithImageName, FileMode.Create);
            await fileStream.CopyToAsync(streamWriter);
        }
        public async Task<HttpResponseMessage> GetContentAsync(string url)
        {
            return await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        }
    }
}
