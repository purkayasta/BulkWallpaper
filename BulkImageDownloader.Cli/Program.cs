using BulkImageDownloader.Cli.Interfaces;
using BulkImageDownloader.Cli.Menu;
using BulkImageDownloader.Cli.Services;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli
{
    class Program
	{
		static async Task Main()
		{
			try
			{
				var serviceCollection = new ServiceCollection();

				ConfigureServices(serviceCollection);

				var serviceProvider = serviceCollection.BuildServiceProvider();

				await MenuViewer.RunAsync(serviceProvider);

			}
			catch (Exception)
			{
				Console.WriteLine("Network Error ! Sorry For this 😔💔");
			}
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddHttpClient(ClientEnum.Unsplash.ToString(), client =>
			{
				client.BaseAddress = new Uri("https://source.unsplash.com/");
			}).AddPolicyHandler(GetRetryPolicy());

			services.AddHttpClient(ClientEnum.Bing.ToString(), client =>
			{
				client.BaseAddress = new Uri("https://www.bing.com");
			}).AddPolicyHandler(GetRetryPolicy());

			services.AddHttpClient(ClientEnum.Pexels.ToString()).AddPolicyHandler(GetRetryPolicy());


			services.AddRefitClient<IBingApi>().ConfigureHttpClient(client =>
			{
				client.BaseAddress = new Uri("https://www.bing.com/");
			});

			services.AddSingleton<IUnsplashService, UnsplashService>();
			services.AddSingleton<IBingService, BingService>();
			services.AddSingleton<IPexelService, PexelService>();
		}

		private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
		{
			return HttpPolicyExtensions.HandleTransientHttpError()
				.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
				.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(5000));
		}
	}
}
