using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BulkImageDownloader.Cli.Interfaces;
using BulkImageDownloader.Cli.ViewModels;
using PexelsDotNetSDK.Api;

namespace BulkImageDownloader.Cli.Services
{
	class PexelService : BaseService, IPexelService
	{
		private readonly PexelsClient _pexelsClient;
		public PexelService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, WallpaperProviderEnum.Pexels)
		{
			_pexelsClient = new PexelsClient("");
		}

		public async Task InitiateDownloadAsync(WallpaperProviderBuilder wallpaperProvider)
		{
			Console.WriteLine("⏬ Downloading .... ");
			string[] urls = wallpaperProvider.Urls;
			var pexelsDirectory = $"{wallpaperProvider.DirectoryLocation}/Pexels/";
			Directory.CreateDirectory(pexelsDirectory);

			int progress = 0;
			int totalCount = wallpaperProvider.NumberOfImages;

			foreach (var url in urls)
			{
				var responses = await GetContentAsync(url);
				await SaveAsync(responses, $"{pexelsDirectory}/{progress}.jpg");

				progress++;
				Console.Write($"\r {progress} | {totalCount}");
			}

			Console.WriteLine("");
			Console.WriteLine("⏬ Downloaded ....  ✔ 💹");
		}

		public async Task<ArrayList> SearchPhotosByNameAsync(string name, int count)
		{
			var photoPage = await _pexelsClient.SearchPhotosAsync(name, pageSize: count);
			Console.WriteLine($"Found : {photoPage.totalResults}");

			ArrayList urls = new();

			if (photoPage == null)
				return urls;

			foreach (var photo in photoPage.photos)
			{
				urls.Add(photo.source.landscape);
			}
			return urls;
		}

		public async Task<ArrayList> GetCurratedImagesAsync(int count)
		{
			var photoPage = await _pexelsClient.CuratedPhotosAsync(pageSize: count);
			Console.WriteLine($"Found : {photoPage.totalResults}");

			ArrayList urls = new();

			if (photoPage == null)
				return urls;

			foreach (var photo in photoPage.photos)
			{
				urls.Add(photo.source.landscape);
			}
			return urls;
		}
	}
}
