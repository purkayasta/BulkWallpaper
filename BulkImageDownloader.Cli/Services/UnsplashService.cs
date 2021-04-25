using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BulkImageDownloader.Cli.Helper;

namespace BulkImageDownloader.Cli.Services
{
	class UnsplashService : BaseService, IDownloadService
	{
		public UnsplashService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, ClientEnums.Unsplash)
		{
		}
		public async Task InitiateDownloadAsync(WallpaperProviderBuilder wallpaperProvider)
		{
			Console.WriteLine("⏬ Downloading .... ");
			string url = wallpaperProvider.UrlPostFix;
			Directory.CreateDirectory("BulkDownlaoder/");
			for (int i = 0; i < wallpaperProvider.NumberOfImages; i++)
			{
				var responses = await GetContentAsync(url);
				await SaveAsync(responses, $"BulkDownlaoder/{DateTime.UtcNow:MM_dd_yyyy}_{i}.png");
				Console.Write($"\r {i} | {wallpaperProvider.NumberOfImages - 1}");
			}
			Console.WriteLine("");
			Console.WriteLine("⏬ Downloaded ....  ✔ 💹");
		}
	}
}
