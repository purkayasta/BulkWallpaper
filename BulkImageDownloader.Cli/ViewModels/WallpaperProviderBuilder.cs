using System.Collections.Generic;

namespace BulkImageDownloader.Cli.ViewModels
{
	public class WallpaperProviderBuilder
	{
		public int NumberOfImages { get; init; }
		public string[] UrlPostFix { get; init; }
		public string DirectoryLocation { get; init; }
		public List<BingImages> BingApiResponses { get; set; }

		public WallpaperProviderBuilder()
		{
			BingApiResponses = new List<BingImages>();
		}
	}
}
