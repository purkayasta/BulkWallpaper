using System;
using System.IO;
using BulkImageDownloader.Cli.Helper.ViewModels;

namespace BulkImageDownloader.Cli.Menu
{
	abstract class MenuBase
	{
		private readonly ClientEnums _wallpaperClient;
		public MenuBase(ClientEnums wallpaperClientEnum)
		{
			_wallpaperClient = wallpaperClientEnum;
		}
		internal virtual int DefaultDownloadableImage { get; set; } = 10;
		internal virtual string DefaultTags { get; set; }
		internal virtual string DefaultDirectory { get; set; } = "BulkImageDownloader/";
		public abstract WallpaperProviderBuilder Build();

		/// Needs Proper Validation
		/// <returns></returns>
		protected int DownloadableImageQuestion()
		{
			/* 
			 * Cannot Accept Unlimited Numbers
			 * Cannot Accept Negative Numbers
			 */
			Console.WriteLine($"📸🎦 How many  images you want to downlaod - (Default: {DefaultDownloadableImage}): ");
			if (int.TryParse(Console.ReadLine(), out int answer))
			{
				if (answer == 0)
				{
					Console.WriteLine($"You want to downlaod {answer} images from {_wallpaperClient} ? 😵😵");
					Console.WriteLine("Please Give a valid answer");
					DownloadableImageQuestion();
				}
				else
				{
					DefaultDownloadableImage = answer;
				}
			}
			else
			{
				Console.WriteLine("Invalid Answer! Please Co-Operate");
				DownloadableImageQuestion();
			}
			return DefaultDownloadableImage;
		}

		/// Needs Proper Validation
		/// <returns></returns>
		protected string WallpaperTypeQuestion()
		{
			/* 
			 * Needs Proper Validation
			 * Cannot Accept Special Characters
			 * Only Accept Comma Separated Text
			 */

			Console.WriteLine($"🌀🏁 Select Tags - (Default : {DefaultTags}): ");
			string answer = Console.ReadLine();
			if (string.IsNullOrEmpty(answer))
				return DefaultTags;
			return answer;
		}

		protected string DownlaodableLocation()
		{
			Console.WriteLine("📩 Where you want to download your images? (Default - Current Directory): ");
			var answer = Console.ReadLine();

			if (!string.IsNullOrEmpty(answer))
			{
				if (Directory.Exists(answer))
					return answer;

				Console.WriteLine("Invalid Directory !");
				DownlaodableLocation();
			}
			return DefaultDirectory;
		}
	}
}
