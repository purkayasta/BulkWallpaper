using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using BulkImageDownloader.Cli.Menu;
using BulkImageDownloader.Cli.Services;
using BulkImageDownloader.Cli.ViewModels;
using Humanizer;

namespace BulkImageDownloader.Cli.Helper
{
	public class MenuViewer
	{
		public static async Task RunAsync(IServiceProvider serviceProvider)
		{
			Stopwatch stopwatch = new();

			try
			{
				Console.OutputEncoding = Encoding.Unicode;

				Console.WriteLine("1. Select Wallpaper Provider (Unsplash, Bing, Pexels) - (Press Enter to Select Default) ");

				var providerAnswer = Console.ReadLine();
				var wallpaperProvider = DetectWallpaperProvider(providerAnswer);
				var wallpaperProviderBuilder = DisplayMenu(wallpaperProvider, serviceProvider);

				stopwatch.Start();
				await Service.InitializeAsync(serviceProvider, wallpaperProviderBuilder, wallpaperProvider);

				Console.WriteLine($"⏲ Total Time Taken : {stopwatch.Elapsed.Humanize()}");
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				stopwatch.Stop();
			}
		}

		public static WallpaperProviderBuilder DisplayMenu(WallpaperProviderEnum wallpaperProvider, IServiceProvider serviceProvider)
		{
			return wallpaperProvider switch
			{
				WallpaperProviderEnum.Unsplash => new UnsplashMenu().Build(),
				WallpaperProviderEnum.Bing => new BingMenu().Build(),
				WallpaperProviderEnum.Pexels => new PexelsMenu(serviceProvider).Build(),
				_ => null,
			};
		}

		public static WallpaperProviderEnum DetectWallpaperProvider(string providerAnswer)
		{
			return providerAnswer.ToLower() switch
			{
				"unsplash" => WallpaperProviderEnum.Unsplash,
				"bing" => WallpaperProviderEnum.Bing,
				"pexels" => WallpaperProviderEnum.Pexels,
				_ => WallpaperProviderEnum.Unsplash,
			};
		}
	}
}
