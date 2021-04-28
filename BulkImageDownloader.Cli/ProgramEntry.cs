using System;
using System.Linq;
using System.Threading.Tasks;
using BulkImageDownloader.Cli.ViewModels;
using BulkImageDownloader.Cli.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BulkImageDownloader.Cli
{
	class ProgramEntry
	{
		internal static async Task InitializeAsync(IServiceProvider serviceProvider, WallpaperProviderBuilder wallpaperProviderBuilder, WallpaperProviderEnum wallpaperProvider)
		{
			switch (wallpaperProvider)
			{
				case WallpaperProviderEnum.Unsplash:
					var unsplashService = serviceProvider.GetRequiredService<IUnsplashService>();
					await StartUnsplashAsync(unsplashService, wallpaperProviderBuilder);
					break;
				case WallpaperProviderEnum.Bing:
					var bingApiService = serviceProvider.GetRequiredService<IBingApi>();
					var bingService = serviceProvider.GetRequiredService<IBingService>();
					await StartBingAsync(bingService, bingApiService, wallpaperProviderBuilder);
					break;
			}

		}

		internal static async Task StartBingAsync(IBingService bingService, IBingApi bingApiService, WallpaperProviderBuilder wallpaperProviderBuilder)
		{
			string[] urlArray = wallpaperProviderBuilder.UrlPostFix;
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
		internal static async Task StartUnsplashAsync(IUnsplashService unsplashService, WallpaperProviderBuilder wallpaperProviderBuilder)
		{
			await unsplashService.InitiateDownloadAsync(wallpaperProviderBuilder);
		}
	}
}
