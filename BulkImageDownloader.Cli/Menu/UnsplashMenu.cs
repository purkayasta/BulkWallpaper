using System;
using BulkImageDownloader.Cli.ViewModels;

namespace BulkImageDownloader.Cli.Menu
{
	internal class UnsplashMenu : MenuBase
	{
		private string _url = string.Empty;
		private bool FeatureImage { get; init; } = true;

		public UnsplashMenu() : base(WallpaperProviderEnum.Unsplash)
		{
			Tags = "Tokyo, Japan, London, seoul, coffee shop, city";
		}

		public override WallpaperProviderBuilder Build()
		{
			WallpaperCountSelector();
			WallpaperTypeSelector();
			var isFeatured = FeatureImageQuestion();
			DirectoryLocationSelector();

			if (isFeatured)
			{
				_url = $"/featured/1920x1024/?{Tags}";
			}
			else
			{
				_url = $"/1920x1024/?{Tags}";
			}

			WallpaperProviderBuilder returnableObject = new()
			{
				NumberOfImages = DownloadableImageCount,
				UrlPostFix = new[] { _url },
				DirectoryLocation = DownloadedDirectory
			};

			return returnableObject;
		}

		private bool FeatureImageQuestion()
		{
			Console.WriteLine($"🌀🏁 Want Currated Images From Unsplash? - (Default : {FeatureImage}): ");
			string answer = Console.ReadLine();

			if (string.IsNullOrEmpty(answer))
				return FeatureImage;

			if (answer.ToLower().Equals("yes") || answer.ToLower().Equals("no"))
			{
				return bool.Parse(answer.ToLower());
			}
			else
			{
				Console.WriteLine("Invalid Answer! ");
				FeatureImageQuestion();
			}
			return false;
		}
	}
}
