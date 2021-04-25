using System;
using System.Collections.Generic;
using BulkImageDownloader.Cli.Helper;

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
	}
}
