using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli.Services
{
	abstract class BaseService
	{
		private readonly ClientEnums _wallpaperClient;
		private readonly IHttpClientFactory _httpClientFactory;
		public BaseService(IHttpClientFactory httpClientFactory, ClientEnums wallpaperClient)
		{
			_httpClientFactory = httpClientFactory;
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
			using var httpClient = _httpClientFactory.CreateClient(_wallpaperClient.ToString());
			return await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
		}
	}
}
