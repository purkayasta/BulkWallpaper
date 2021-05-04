using BulkImageDownloader.Cli.Services;
using BulkImageDownloader.Cli.ViewModels;
using Humanizer;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli.Menu
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

		public static WallpaperModel DisplayMenu(ClientEnum wallpaperProvider, IServiceProvider serviceProvider)
		{
			return wallpaperProvider switch
			{
				ClientEnum.Unsplash => new UnsplashMenu().Build(),
				ClientEnum.Bing => new BingMenu().Build(),
				ClientEnum.Pexels => new PexelsMenu(serviceProvider).Build(),
				_ => throw new NotImplementedException("This Type Not Found")
			};
		}

		public static ClientEnum DetectWallpaperProvider(string providerAnswer)
		{
			return providerAnswer.ToLower() switch
			{
				"unsplash" => ClientEnum.Unsplash,
				"bing" => ClientEnum.Bing,
				"pexels" => ClientEnum.Pexels,
				_ => ClientEnum.Unsplash,
			};
		}
	}
}
