using BulkImageDownloader.Cli.ViewModels;
using System.Collections.Generic;

namespace BulkImageDownloader.Cli.Menu
{
    public class BingMenu : MenuBase
    {
        private static int _index = 0;
        private static int _numberOfImages = 7;

        private string _url = $"HPImageArchive.aspx?format=js&idx={_index}&n={_numberOfImages}&mkt=en-US";
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
