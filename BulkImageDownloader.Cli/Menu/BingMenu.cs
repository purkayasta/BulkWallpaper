

using BulkImageDownloader.Cli.ViewModels;

namespace BulkImageDownloader.Cli.Menu
{
	class BingMenu : MenuBase
	{
		private static int _index = 0;
		private static int _numberOfImages = 7;

		private string _url = $"HPImageArchive.aspx?format=js&idx={_index}&n={_numberOfImages}&mkt=en-US";
		public BingMenu() : base(WallpaperProviderEnum.Bing)
		{
			DownloadableImageCount = 7;
			SpecialRules = "Max = 14";
			MaxDownloadLimit = 14;
		}

		public override WallpaperProviderBuilder Build()
		{
			WallpaperCountSelector();
			DirectoryLocationSelector();
			var urlArray = BuildUrl();

			WallpaperProviderBuilder returnableObject = new()
			{
				NumberOfImages = DownloadableImageCount,
				UrlPostFix = urlArray,
				DirectoryLocation = DownloadedDirectory
			};

			return returnableObject;
		}
		private string[] BuildUrl()
		{
			string[] urlArray = new string[2];
			urlArray[0] = _url;

			if (DownloadableImageCount > 7)
			{
				_index = 8;
				_numberOfImages = DownloadableImageCount - 7;
				_url = $"HPImageArchive.aspx?format=js&idx={_index}&n={_numberOfImages}&mkt=en-US";
				urlArray[1] = _url;
			}

			return urlArray;
		}
	}
}
