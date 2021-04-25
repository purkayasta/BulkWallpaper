using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BulkImageDownloader.Cli.Helper.ViewModels;
using BulkImageDownloader.Cli.Interfaces;

namespace BulkImageDownloader.Cli.Services
{
	class BingService : BaseService, IDownloadService
	{
		public BingService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, ClientEnums.Bing)
		{
		}
		public async Task InitiateDownloadAsync(WallpaperProviderBuilder wallpaperProvider)
		{
			if (wallpaperProvider.BingApiResponses.Count < 0)
			{
				Console.WriteLine("Network Error! Please Try Again...");
				return;
			}

			Console.WriteLine("⏬ Downloading .... ");

			var directory = $"{wallpaperProvider.DirectoryLocation}/Bing/";

			Directory.CreateDirectory(directory);

			int totalCount = wallpaperProvider.BingApiResponses.Count;
			int progress = 0;

			foreach (var item in wallpaperProvider.BingApiResponses)
			{
				var responses = await GetContentAsync(item.Url);
				await SaveAsync(responses, $"{directory}/{item.Name}.png");

				progress++;
				Console.Write($"\r {progress} | {totalCount}");
			}

			Console.WriteLine("");
			Console.WriteLine("⏬ Downloaded ....  ✔ 💹");
		}
	}
}
