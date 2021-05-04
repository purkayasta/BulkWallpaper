using System.Collections.Generic;

namespace BulkImageDownloader.Cli.ViewModels
{
    public struct WallpaperModel
    {
        public WallpaperModel(int numberOfImages, string[] urls, List<ImageInfo> imageInfos, string directoryLocation, List<BingImages> bingApiResponses)
        {
            NumberOfImages = numberOfImages;
            Urls = urls;
            ImageInfos = imageInfos;
            DirectoryLocation = directoryLocation;
            BingApiResponses = bingApiResponses;
        }

        public int NumberOfImages { get; init; }
        public string[] Urls { get; init; }
        public List<ImageInfo> ImageInfos { get; init; }
        public string DirectoryLocation { get; init; }
        public List<BingImages> BingApiResponses { get; set; }
    }
    public struct ImageInfo
    {
        public string Name { get; set; }

        public ImageInfo(string name, string url) : this()
        {
            Name = name;
            Url = url;
        }

        public string Url { get; set; }
    }
}
