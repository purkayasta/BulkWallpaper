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
		public static async Task InitializeAsync(IServiceProvider serviceProvider, WallpaperProviderBuilder wallpaperProviderBuilder, WallpaperProviderEnum wallpaperProvider)
		{
			switch (wallpaperProvider)
			{
				case WallpaperProviderEnum.Unsplash:
					var unsplashService = serviceProvider.GetRequiredService<IUnsplashService>();
					await StartDownloadService(unsplashService, wallpaperProviderBuilder);
					break;
				case WallpaperProviderEnum.Bing:
					var bingApiService = serviceProvider.GetRequiredService<IBingApi>();
					var bingService = serviceProvider.GetRequiredService<IBingService>();
					await StartBingAsync(bingService, bingApiService, wallpaperProviderBuilder);
					break;
				case WallpaperProviderEnum.Pexels:
					var pexelService = serviceProvider.GetRequiredService<IPexelService>();
					await StartDownloadService(pexelService, wallpaperProviderBuilder);
					break;
			}

		}

		public static async Task StartBingAsync(IBingService bingService, IBingApi bingApiService, WallpaperProviderBuilder wallpaperProviderBuilder)
		{
			string[] urlArray = wallpaperProviderBuilder.Urls;
			foreach (var url in urlArray)
			{
				if (!string.IsNullOrEmpty(url))
				{
					var responses = await bingApiService.GetResponseAsync(url);
					if (responses.Images.Count > 0)
					{
						wallpaperProviderBuilder.BingApiResponses.AddRange(responses.Images.ToList());
					}
				}

			}
			if (wallpaperProviderBuilder.BingApiResponses.Count > 0)
				await bingService.InitiateDownloadAsync(wallpaperProviderBuilder);
			else
				Console.WriteLine("No Valid Url Found To Download 😭😭");
		}
		public static async Task StartDownloadService(IDownloadService wallpaperService, WallpaperProviderBuilder wallpaperProviderBuilder)
		{
			await wallpaperService.InitiateDownloadAsync(wallpaperProviderBuilder);
		}
	}
}
