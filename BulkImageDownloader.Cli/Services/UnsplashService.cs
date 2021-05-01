using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BulkImageDownloader.Cli.ViewModels;
using BulkImageDownloader.Cli.Interfaces;

namespace BulkImageDownloader.Cli.Services
{
	class UnsplashService : BaseService, IUnsplashService
	{
		public UnsplashService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, WallpaperProviderEnum.Unsplash)
		{
		}
		public async Task InitiateDownloadAsync(WallpaperProviderBuilder wallpaperProvider)
		{
			Console.WriteLine("⏬ Downloading .... ");
			string[] urls = wallpaperProvider.Urls;
			var unsplashDirectory = $"{wallpaperProvider.DirectoryLocation}/Unsplash/";
			Directory.CreateDirectory(unsplashDirectory);

			int progress = 0;
			int totalCount = wallpaperProvider.NumberOfImages;

			foreach (var url in urls)
			{
				// hitting same url for random images
				for (int i = 0; i < totalCount; i++)
				{
					var responses = await GetContentAsync(url);
					await SaveAsync(responses, $"{unsplashDirectory}/{DateTime.UtcNow:MM_dd_yyyy}_{i}.png");

					progress++;
					Console.Write($"\r {progress} | {totalCount}");
					Thread.Sleep(2000);
				}
			}

			Console.WriteLine("");
			Console.WriteLine("⏬ Downloaded ....  ✔ 💹");
		}
	}
}
