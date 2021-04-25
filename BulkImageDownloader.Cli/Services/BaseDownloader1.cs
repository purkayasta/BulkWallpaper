using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli.Services
{
	class BaseDownloader1
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public BaseDownloader1(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task DownloadAsync(string url, string localPathWithImageName)
		{
			using var httpClient = _httpClientFactory.CreateClient();
			using var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
			using var fileStream = await response.Content.ReadAsStreamAsync();
			using var streamWriter = File.Open(localPathWithImageName, FileMode.Create);
			await fileStream.CopyToAsync(streamWriter);
		}
	}
}
