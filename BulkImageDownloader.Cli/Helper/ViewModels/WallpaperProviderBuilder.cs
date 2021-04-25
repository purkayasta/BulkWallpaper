using System.Collections.Generic;

namespace BulkImageDownloader.Cli.Helper.ViewModels
{
	public class WallpaperProviderBuilder
	{
		public int NumberOfImages { get; init; }
		public string UrlPostFix { get; init; }
		public string DirectoryLocation { get; init; }
		public IReadOnlyList<Images> BingApiResponses { get; set; }
	}
}
