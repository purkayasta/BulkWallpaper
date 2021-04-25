using BulkImageDownloader.Cli.Helper.ViewModels;

namespace BulkImageDownloader.Cli.Menu
{
	class BingMenu : MenuBase
	{
		private readonly string _url = "HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US";
		public BingMenu() : base(ClientEnums.Bing)
		{
		}

		public override WallpaperProviderBuilder Build()
		{
			var numberOfImage = DownloadableImageQuestion();
			var directory = DownlaodableLocation();

			WallpaperProviderBuilder returnableObject = new()
			{
				NumberOfImages = numberOfImage,
				UrlPostFix = _url.Replace("1", $"{numberOfImage}"),
				DirectoryLocation = directory
			};

			return returnableObject;
		}
		
	}
}
