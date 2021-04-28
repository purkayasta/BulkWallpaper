using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using BulkImageDownloader.Cli.ViewModels;
using BulkImageDownloader.Cli.Interfaces;
using BulkImageDownloader.Cli.Menu;
using BulkImageDownloader.Cli.Services;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Refit;

namespace BulkImageDownloader.Cli
{
	class Program
	{
		static Stopwatch _stopwatch;
		static async Task Main()
		{
			try
			{
				_stopwatch = new Stopwatch();
				var serviceCollection = new ServiceCollection();

				ConfigureServices(serviceCollection);

				var serviceProvider = serviceCollection.BuildServiceProvider();

				await StartAsync(serviceProvider);

			}
			catch (Exception)
			{
				Console.WriteLine("Something Bad Happened!!");
			}
			finally
			{
				_stopwatch.Stop();
			}
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddHttpClient(WallpaperProviderEnum.Unsplash.ToString(), client =>
			{
				client.BaseAddress = new Uri("https://source.unsplash.com/");
			}).AddPolicyHandler(GetRetryPolicy());

			services.AddHttpClient(WallpaperProviderEnum.Bing.ToString(), client =>
			{
				client.BaseAddress = new Uri("https://www.bing.com");
			}).AddPolicyHandler(GetRetryPolicy());

			services.AddRefitClient<IBingApi>().ConfigureHttpClient(client =>
			{
				client.BaseAddress = new Uri("https://www.bing.com/");
			});

			services.AddScoped<IUnsplashService, UnsplashService>();
			services.AddScoped<IBingService, BingService>();
		}

		private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
		{
			return HttpPolicyExtensions.HandleTransientHttpError()
				.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
				.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(5000));
		}


		private static async Task StartAsync(IServiceProvider serviceProvider)
		{
			Console.WriteLine("1. Select Wallpaper Provider (Unsplash, Bing) - (Press Enter to Select Default) ");
			var providerAnswer = Console.ReadLine();
			var wallpaperProvider = DetectWallpaperProvider(providerAnswer);
			var wallpaperProviderBuilder = DisplayMenu(wallpaperProvider);

			_stopwatch.Start();
			await ProgramEntry.InitializeAsync(serviceProvider, wallpaperProviderBuilder, wallpaperProvider);
			Console.WriteLine($"⏲ Total Time Taken : {_stopwatch.Elapsed.Humanize()}");
		}


		private static WallpaperProviderBuilder DisplayMenu(WallpaperProviderEnum wallpaperProvider)
		{
			return wallpaperProvider switch
			{
				WallpaperProviderEnum.Unsplash => new UnsplashMenu().Build(),
				WallpaperProviderEnum.Bing => new BingMenu().Build(),
				_ => null,
			};
		}

		private static WallpaperProviderEnum DetectWallpaperProvider(string providerAnswer)
		{
			return providerAnswer.ToLower() switch
			{
				"unsplash" => WallpaperProviderEnum.Unsplash,
				"bing" => WallpaperProviderEnum.Bing,
				_ => WallpaperProviderEnum.Unsplash,
			};
		}
	}
}
