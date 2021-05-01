using System;
using System.Collections.Generic;
using BulkImageDownloader.Cli.Interfaces;
using BulkImageDownloader.Cli.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BulkImageDownloader.Cli.Menu
{
    public class PexelsMenu : MenuBase
    {
        private readonly IPexelService _pexelService;

        public bool FeatureImage { get; set; } = true;
        public PexelsMenu(IServiceProvider serviceProvider) : base(ClientEnum.Pexels)
        {
            SpecialDownloadCountRules = "Max = 20";
            MaxDownloadLimit = 20;
            Tags = "4k wallpaper";
            SpecialTagRules = "Provide one category only, If you provide more than one it will select the first one!";
            _pexelService = serviceProvider.GetRequiredService<IPexelService>();
        }

        public override WallpaperModel Build()
        {
            WallpaperCountSelector();
            CurratedOnlyWallpaper();
            var urls = BuildUrls(FeatureImage);

            DirectoryLocationSelector();

            WallpaperModel returnableObject = new()
            {
                NumberOfImages = DownloadableImageCount,
                ImageInfos = urls,
                DirectoryLocation = DownloadedDirectory
            };

            return returnableObject;
        }
        public bool CurratedOnlyWallpaper()
        {
            Console.WriteLine($"🌀🏁 Want Only Currated Images From Pexels? - (Default : {FeatureImage}): ");
            string answer = Console.ReadLine();

            if (string.IsNullOrEmpty(answer))
                return FeatureImage;

            if (answer.ToLower().Equals("true") || answer.ToLower().Equals("false"))
            {
                FeatureImage = bool.Parse(answer.ToLower());
            }
            else
            {
                Console.WriteLine("Invalid Answer! ");
                CurratedOnlyWallpaper();
            }
            return FeatureImage;
        }

        public List<ImageInfo> BuildUrls(bool isFeatureImage)
        {
            if (isFeatureImage)
                return BuildUrlWithFeatureImages(DownloadableImageCount);

            return BuildUrlsWithSearchedName(DownloadableImageCount);
        }

        public List<ImageInfo> BuildUrlsWithSearchedName(int count)
        {
            WallpaperTypeSelector();

            var urls = _pexelService.SearchPhotosByNameAsync(Tags, count).GetAwaiter().GetResult();

            if (urls.Count < 0)
            {
                Console.WriteLine($"No Images Found With ==> {Tags}");
                BuildUrlsWithSearchedName(count);
            }

            return urls;
        }

        public List<ImageInfo> BuildUrlWithFeatureImages(int count)
        {
            var urls = _pexelService.GetCurratedImagesAsync(count).GetAwaiter().GetResult();

            return urls;
        }
    }
}
