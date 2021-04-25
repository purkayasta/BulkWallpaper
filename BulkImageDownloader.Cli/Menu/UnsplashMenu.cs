using System;
using BulkImageDownloader.Cli.Helper.ViewModels;

namespace BulkImageDownloader.Cli.Menu
{
	internal class UnsplashMenu : MenuBase
	{
		private string _url = string.Empty;
		private bool FeatureImage { get; init; } = true;

		public UnsplashMenu() : base(ClientEnums.Unsplash)
		{
			DefaultTags = "nature, water";
		}

		public override WallpaperProviderBuilder Build()
		{
			var numberOfImage = DownloadableImageQuestion();
			var selectedTags = WallpaperTypeQuestion();
			var isFeatured = FeatureImageQuestion();

			if (isFeatured)
			{
				_url = $"/featured/1920x1024/?{selectedTags}";
			}
			else
			{
				_url = $"/1920x1024/?{selectedTags}";
			}

			WallpaperProviderBuilder returnableObject = new()
			{
				NumberOfImages = numberOfImage,
				UrlPostFix = _url
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
