using System.Collections.Generic;

namespace BulkImageDownloader.Cli.ViewModels
{
    public struct WallpaperModel
    {
        public int NumberOfImages { get; init; }
        public string[] Urls { get; init; }
        public List<ImageInfo> ImageInfos { get; init; }
        public string DirectoryLocation { get; init; }
        public List<BingImages> BingApiResponses { get; set; }

    }
    public struct ImageInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
