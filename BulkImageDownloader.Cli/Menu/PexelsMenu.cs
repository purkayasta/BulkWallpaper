using System;
using BulkImageDownloader.Cli.Interfaces;
using BulkImageDownloader.Cli.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BulkImageDownloader.Cli.Menu
{
	class PexelsMenu : MenuBase
	{
		private string[] _urls;
		private readonly IPexelService _pexelService;

		private bool FeatureImage { get; init; } = true;
		public PexelsMenu(IServiceProvider serviceProvider) : base(WallpaperProviderEnum.Pexels)
		{
			SpecialDownloadCountRules = "Max = 20";
			MaxDownloadLimit = 20;
			Tags = "4k wallpaper";
			SpecialTagRules = "Provide one category only, If you provide more than one it will select the first one!";
			_pexelService = serviceProvider.GetRequiredService<IPexelService>();
		}

		public override WallpaperProviderBuilder Build()
		{
			WallpaperCountSelector();
			CurratedOnlyWallpaper();
			_urls = BuildUrls(FeatureImage);

			DirectoryLocationSelector();

			WallpaperProviderBuilder returnableObject = new()
			{
				NumberOfImages = DownloadableImageCount,
				Urls = _urls,
				DirectoryLocation = DownloadedDirectory
			};

			return returnableObject;
		}
		public bool CurratedOnlyWallpaper()
		{
			Console.WriteLine($"🌀🏁 Want Only Currated Images From Pexels? - (Default : {FeatureImage}): ");
			string answer = Console.ReadLine();

			if (string.IsNullOrEmpty(answer))
				return FeatureImage;

			if (answer.ToLower().Equals("true") || answer.ToLower().Equals("false"))
			{
				return bool.Parse(answer.ToLower());
			}
			else
			{
				Console.WriteLine("Invalid Answer! ");
				CurratedOnlyWallpaper();
			}
			return false;
		}

		private string[] BuildUrls(bool isFeatureImage)
		{
			if (isFeatureImage)
				return BuildUrlWithFeatureImages(DownloadableImageCount);

			return BuildUrlsWithSearchedName(DownloadableImageCount);
		}

		private string[] BuildUrlsWithSearchedName(int count)
		{
			WallpaperTypeSelector();

			var urls = _pexelService.SearchPhotosByNameAsync(Tags, count).GetAwaiter().GetResult();

			if (urls.Count < 0)
			{
				Console.WriteLine($"No Images Found With ==> {Tags}");
				BuildUrlsWithSearchedName(count);
			}

			return (string[])urls.ToArray();
		}

		private string[] BuildUrlWithFeatureImages(int count)
		{
			var urls = _pexelService.GetCurratedImagesAsync(count).GetAwaiter().GetResult();

			return (string[])urls.ToArray(typeof(string));
		}
	}
}
