using System;
using System.Linq;
using System.Threading.Tasks;
using BulkImageDownloader.Cli.ViewModels;
using BulkImageDownloader.Cli.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BulkImageDownloader.Cli.Services
{
	public class Service
	{
		public static async Task InitializeAsync(IServiceProvider serviceProvider, WallpaperModel wallpaperModel, ClientEnum clientEnum)
		{
			switch (clientEnum)
			{
				case ClientEnum.Unsplash:
					var unsplashService = serviceProvider.GetRequiredService<IUnsplashService>();
					await StartDownloadService(unsplashService, wallpaperModel);
					break;
				case ClientEnum.Bing:
					var bingApiService = serviceProvider.GetRequiredService<IBingApi>();
					var bingService = serviceProvider.GetRequiredService<IBingService>();
					await StartBingAsync(bingService, bingApiService, wallpaperModel);
					break;
				case ClientEnum.Pexels:
					var pexelService = serviceProvider.GetRequiredService<IPexelService>();
					await StartDownloadService(pexelService, wallpaperModel);
					break;
			}
		}

		public static async Task StartBingAsync(IBingService bingService, IBingApi bingApiService, WallpaperModel wallpaperModel)
		{
			string[] urlArray = wallpaperModel.Urls;
			foreach (var url in urlArray)
			{
				if (!string.IsNullOrEmpty(url))
				{
					var responses = await bingApiService.GetResponseAsync(url);
					if (responses.Images.Count > 0)
					{
						wallpaperModel.BingApiResponses.AddRange(responses.Images.ToList());
					}
				}

			}
			if (wallpaperModel.BingApiResponses.Count > 0)
				await bingService.InitiateDownloadAsync(wallpaperModel);
			else
				Console.WriteLine("No Valid Url Found To Download 😭😭");
		}
		public static async Task StartDownloadService(IDownloadService wallpaperService, WallpaperModel wallpaperModel)
		{
			await wallpaperService.InitiateDownloadAsync(wallpaperModel);
		}
	}
}
