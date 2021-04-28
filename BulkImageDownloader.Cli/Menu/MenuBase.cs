using System;
using System.IO;
using BulkImageDownloader.Cli.ViewModels;

namespace BulkImageDownloader.Cli.Menu
{
	abstract class MenuBase
	{
		private readonly WallpaperProviderEnum _wallpaperClient;
		public MenuBase(WallpaperProviderEnum wallpaperClientEnum)
		{
			_wallpaperClient = wallpaperClientEnum;
		}
		internal virtual int DownloadableImageCount { get; set; } = 10;
		internal virtual string Tags { get; set; }
		internal virtual string DownloadedDirectory { get; set; } = "BulkImageDownloader/";
		internal virtual string SpecialRules { get; set; }
		internal virtual int MaxDownloadLimit { get; set; } = 0;
		public abstract WallpaperProviderBuilder Build();

		/// Needs Proper Validation
		/// <returns></returns>
		protected void WallpaperCountSelector()
		{
			/* 
			 * Cannot Accept Unlimited Numbers
			 * Cannot Accept Negative Numbers
			 */
			Console.Write($"📸🎦 How many  images you want to downlaod - (Default: {DownloadableImageCount} {SpecialRules}): ");

			string input = Console.ReadLine();
			Console.WriteLine("");

			if (string.IsNullOrWhiteSpace(input))
			{
				return;
			}

			if (int.TryParse(input, out int answer))
			{
				if (MaxDownloadLimit > 0)
				{
					if (answer > MaxDownloadLimit)
					{
						Console.WriteLine("Max Download Limit Crossed! ❌❌");
						Console.WriteLine("Please Provide a value within the range!! ");
						WallpaperCountSelector();
					}
				}

				if (answer > 0)
				{
					DownloadableImageCount = answer;
					return;
				}
			}

			Console.WriteLine($"You want to downlaod {answer} images from {_wallpaperClient} ? 😵😵");
			Console.WriteLine("Please provide a valid answer 🙏");
			WallpaperCountSelector();
		}

		/// Needs Proper Validation
		/// <returns></returns>
		protected void WallpaperTypeSelector()
		{
			/* 
			 * Needs Proper Validation
			 * Cannot Accept Special Characters
			 * Only Accept Comma Separated Text
			 */
			Console.Write($"🌀🏁 Select Tags - (Default : {Tags}): ");
			string answer = Console.ReadLine();

			Console.WriteLine("");

			if (string.IsNullOrEmpty(answer))
				return;

			Tags = answer;
		}

		protected void DirectoryLocationSelector()
		{
			Console.Write("📩 Where you want to download your images? (Default - Current Directory): ");
			var answer = Console.ReadLine();

			Console.WriteLine("");

			if (!string.IsNullOrEmpty(answer))
			{
				if (Directory.Exists(answer))
				{
					var diretory = Directory.CreateDirectory(answer + "/" + DownloadedDirectory);

					DownloadedDirectory = diretory.FullName;

					return;
				}

				Console.WriteLine("Invalid Directory !");
				DirectoryLocationSelector();
			}
		}
	}
}
