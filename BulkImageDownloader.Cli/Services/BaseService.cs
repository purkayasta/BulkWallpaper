using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli.Services
{
	abstract class BaseService
	{
		private readonly WallpaperProviderEnum _wallpaperClient;
		private readonly HttpClient _httpClient;
		public BaseService(IHttpClientFactory httpClientFactory, WallpaperProviderEnum wallpaperClient)
		{
			_httpClient = httpClientFactory.CreateClient(_wallpaperClient.ToString());
			_wallpaperClient = wallpaperClient;
		}
		protected async Task SaveAsync(HttpResponseMessage response, string localPathWithImageName)
		{
			using var fileStream = await response.Content.ReadAsStreamAsync();
			using var streamWriter = File.Open(localPathWithImageName, FileMode.Create);
			await fileStream.CopyToAsync(streamWriter);
		}
		protected async Task<HttpResponseMessage> GetContentAsync(string url)
		{
			return await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
		}

	}
}
