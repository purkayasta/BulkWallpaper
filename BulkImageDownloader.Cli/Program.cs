using System;
using System.Net.Http;
using System.Threading.Tasks;
using BulkImageDownloader.Cli.Helper;
using BulkImageDownloader.Cli.Menu;
using BulkImageDownloader.Cli.Services;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace BulkImageDownloader.Cli
{
	class Program
	{
		static async Task Main()
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddHttpClient(ClientEnums.Unsplash.ToString(), client =>
			{
				client.BaseAddress = new Uri("https://source.unsplash.com/");
			}).AddPolicyHandler(GetRetryPolicy());


			serviceCollection.AddScoped<IDownloadService, UnsplashService>();
			var serviceProvider = serviceCollection.BuildServiceProvider();


			await StartAsync(serviceProvider);
		}
		private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
		{
			return HttpPolicyExtensions.HandleTransientHttpError()
				.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
				.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(5000));
		}

		private static async Task StartAsync(IServiceProvider serviceProvider)
		{
			Console.WriteLine("1. Select Wallpaper Provider (Unsplash, Bing, Pexels) - ");
			var providerAnswer = Console.ReadLine();
			var wallpaperProvider = DetectWallpaperProvider(providerAnswer);
			var wallpaperProviderBuilder = DisplayMenu(wallpaperProvider);
			var services = serviceProvider.GetRequiredService<IDownloadService>();
			await services.InitiateDownloadAsync(wallpaperProviderBuilder);
		}

		private static WallpaperProviderBuilder DisplayMenu(ClientEnums wallpaperProvider)
		{
			return wallpaperProvider switch
			{
				ClientEnums.Unsplash => new UnsplashMenu().Build(),
				_ => null,
			};
		}


		private static ClientEnums DetectWallpaperProvider(string providerAnswer)
		{
			return providerAnswer.ToLower() switch
			{
				"unsplash" => ClientEnums.Unsplash,
				"bing" => ClientEnums.Bing,
				_ => ClientEnums.Unsplash,
			};
		}
	}
}
