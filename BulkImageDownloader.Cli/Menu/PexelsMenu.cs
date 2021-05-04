using BulkImageDownloader.Cli.Config;
using BulkImageDownloader.Cli.Interfaces;
using BulkImageDownloader.Cli.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BulkImageDownloader.Cli.Menu
{
    public class PexelsMenu : BaseMenu
	{
		private readonly IPexelService _pexelService;

		public bool FeatureImage { get; set; } = true;
		public PexelsMenu(IServiceProvider serviceProvider) : base(ClientEnum.Pexels)
		{
			SpecialDownloadCountRules = "Max = 20";
			MaxDownloadLimit = 20;
			Tags = "4k wallpaper";
			SpecialTagRules = "Provide one category only, If you provide more than one it will select the first one!";
			_pexelService = serviceProvider.GetRequiredService<IPexelService>();
		}

		public override WallpaperModel Build()
		{
			VerifyAccessKey();
			WallpaperCountSelector();
			CurratedOnlyWallpaper();
			var urls = BuildUrls(FeatureImage);

			DirectoryLocationSelector();

			WallpaperModel returnableObject = new()
			{
				NumberOfImages = DownloadableImageCount,
				ImageInfos = urls,
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
				FeatureImage = bool.Parse(answer.ToLower());
			}
			else
			{
				Console.WriteLine("Invalid Answer! ");
				CurratedOnlyWallpaper();
			}
			return FeatureImage;
		}

		public List<ImageInfo> BuildUrls(bool isFeatureImage)
		{
			if (isFeatureImage)
				return BuildUrlWithFeatureImages(DownloadableImageCount);

			return BuildUrlsWithSearchedName(DownloadableImageCount);
		}

		public List<ImageInfo> BuildUrlsWithSearchedName(int count)
		{
			WallpaperTypeSelector();

			var urls = _pexelService.SearchPhotosByNameAsync(Tags, count).GetAwaiter().GetResult();

			if (urls.Count < 0)
			{
				Console.WriteLine($"No Images Found With ==> {Tags}");
				BuildUrlsWithSearchedName(count);
			}

			return urls;
		}

		public List<ImageInfo> BuildUrlWithFeatureImages(int count)
		{
			var urls = _pexelService.GetCurratedImagesAsync(count).GetAwaiter().GetResult();

			return urls;
		}

		public void VerifyAccessKey()
		{
			string accessKey = ConfigManager.GetSection("Pexels")["Key"];

			if (string.IsNullOrEmpty(accessKey))
			{
				accessKey = AskForKey();
			}
			else
			{
				accessKey = UsePreviousKey(accessKey);
			}

			InitiateConnection(accessKey);
		}

		public void InitiateConnection(string accessKey)
		{
			bool connected = _pexelService.InitiateConnection(accessKey);

			if (!connected)
			{
				UpdateKeyToConfig(accessKey);
				VerifyAccessKey();
			}
		}

		public void UpdateKeyToConfig(string key)
		{
			ConfigManager.UpdateAppSettings("Pexels:Key", key);
		}

		public string AskForKey()
		{
			Console.WriteLine("Please Provide a access key");
			string key = GetUserInput();

			if (string.IsNullOrEmpty(key))
			{
				Console.WriteLine("Please Co-Operate 🙏");
				AskForKey();
			}

			UpdateKeyToConfig(key);

			return key;
		}

		public string UsePreviousKey(string key)
		{
			Console.WriteLine("Use Previous key ? (> yes / no) Select Enter For Default (yes)");

			string answer = GetUserInput();

			if (string.IsNullOrEmpty(answer))
			{
				return key;
			}

			answer = answer.ToLower();

			if (answer.Equals("no"))
			{
				return AskForKey();
			}
			else if (answer.Equals("yes"))
			{
				return key;
			}
			else
			{
				UsePreviousKey(key);
			}
			return key;
		}
	}
}
