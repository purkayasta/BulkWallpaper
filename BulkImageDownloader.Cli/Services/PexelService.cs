using BulkImageDownloader.Cli.Interfaces;
using BulkImageDownloader.Cli.ViewModels;
using PexelsDotNetSDK.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli.Services
{
    public class PexelService : BaseService, IPexelService
	{
		private PexelsClient _pexelsClient;
		public PexelService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, ClientEnum.Pexels)
		{

		}

		public bool InitiateConnection(string accessKey)
		{
			bool isConnected = true;
			try
			{
				_pexelsClient = new PexelsClient(accessKey);
			}
			catch (Exception)
			{
				Console.WriteLine("Access Key is Invalid ❌❌");
				isConnected = false;
			}
			return isConnected;
		}

		public async Task InitiateDownloadAsync(WallpaperModel wallpaperModel)
		{
			Console.WriteLine("⏬ Downloading .... ");
			var imageInfos = wallpaperModel.ImageInfos;
			var pexelsDirectory = $"{wallpaperModel.DirectoryLocation}/Pexels/";
			Directory.CreateDirectory(pexelsDirectory);

			int progress = 0;
			int totalCount = wallpaperModel.NumberOfImages;

			foreach (var image in imageInfos)
			{
				var responses = await GetContentAsync(image.Url);
				await SaveAsync(responses, $"{pexelsDirectory}/{image.Name}");

				progress++;
				Console.Write($"\r {progress} | {totalCount}");
			}

			Console.WriteLine("");
			Console.WriteLine("⏬ Downloaded ....  ✔ 💹");
		}

		public async Task<List<ImageInfo>> SearchPhotosByNameAsync(string name, int count)
		{
			var photoPage = await _pexelsClient.SearchPhotosAsync(name, pageSize: count);

			List<ImageInfo> imagesInfo = new();

			if (photoPage == null)
				return imagesInfo;

			foreach (var photo in photoPage.photos)
			{
				imagesInfo.Add(new ImageInfo
				{
					Url = photo.source.landscape,
					Name = photo.id.ToString() + ".jpg"
				});
			}
			return imagesInfo;
		}

		public async Task<List<ImageInfo>> GetCurratedImagesAsync(int count)
		{
			var photoPage = await _pexelsClient.CuratedPhotosAsync(pageSize: count);

			List<ImageInfo> imagesInfo = new();

			if (photoPage == null)
				return imagesInfo;

			foreach (var photo in photoPage.photos)
			{
				imagesInfo.Add(new ImageInfo
				{
					Url = photo.source.landscape,
					Name = photo.id.ToString() + ".jpg"
				});
			}
			return imagesInfo;
		}
	}
}
