using System.Collections.Generic;
using BulkImageDownloader.Cli.ViewModels;

namespace BulkImageDownloader.Cli.Menu
{
	public class BingMenu : BaseMenu
    {
		//private static int _index = 0;
        private static int _numberOfImages = 7;

		//public string url = $"HPImageArchive.aspx?format=js&idx={_index}&n={_numberOfImages}&mkt=en-US";
		private const string _baseUrl = "HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US";
        public virtual string Url { get; set; } = _baseUrl;

        public BingMenu() : base(ClientEnum.Bing)
        {
            DownloadableImageCount = 7;
            SpecialDownloadCountRules = "Max = 14";
            MaxDownloadLimit = 14;
        }

        public override WallpaperModel Build()
        {
            WallpaperCountSelector();
            DirectoryLocationSelector();
            var urlArray = BuildUrl();

            WallpaperModel returnableObject = new()
            {
                NumberOfImages = DownloadableImageCount,
                Urls = urlArray,
                DirectoryLocation = DownloadedDirectory,
                BingApiResponses = new List<BingImages>()
            };

            return returnableObject;
        }
        public string[] BuildUrl()
        {
            string[] urlArray = new string[2];
            urlArray[0] = Url.Replace('1', '7');

            if (DownloadableImageCount > 7)
            {
                _numberOfImages = DownloadableImageCount - 7;
                Url = Url.Replace('0', '8').Replace('1', (char)_numberOfImages);
                urlArray[1] = Url;
            }

            return urlArray;
        }
    }
}
