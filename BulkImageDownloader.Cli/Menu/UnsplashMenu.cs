using System;
using System.Collections.Generic;
using BulkImageDownloader.Cli.ViewModels;

namespace BulkImageDownloader.Cli.Menu
{
    public class UnsplashMenu : MenuBase
    {
        private string _url = string.Empty;
        public bool FeatureImage { get; set; } = true;

        public UnsplashMenu() : base(ClientEnum.Unsplash)
        {
            Tags = "Japan, London, coffee shop, 4k wallpapers";
        }

        public override WallpaperModel Build()
        {
            WallpaperCountSelector();
            WallpaperTypeSelector();
            FeatureImageQuestion();
            DirectoryLocationSelector();

            if (FeatureImage)
            {
                _url = $"/featured/1920x1024/?{Tags}";
            }
            else
            {
                _url = $"/1920x1024/?{Tags}";
            }

            var urls = BuildUrls(DownloadableImageCount, _url);


            WallpaperModel returnableObject = new()
            {
                NumberOfImages = DownloadableImageCount,
                Urls = new[] { _url },
                ImageInfos = urls,
                DirectoryLocation = DownloadedDirectory
            };

            return returnableObject;
        }

        public void FeatureImageQuestion()
        {
            Console.WriteLine($"🌀🏁 Want Currated Images From Unsplash? - (Default : {FeatureImage}): ");
            string answer = Console.ReadLine();

            if (string.IsNullOrEmpty(answer))
                return;

            if (answer.ToLower().Equals("yes") || answer.ToLower().Equals("no"))
            {
                FeatureImage = bool.Parse(answer.ToLower());
            }
            else
            {
                Console.WriteLine("Invalid Answer! ");
                FeatureImageQuestion();
            }
            return;
        }

        public List<ImageInfo> BuildUrls(int count, string url)
        {
            List<ImageInfo> imageInfos = new List<ImageInfo>();
            for (int i = 0; i < count; i++)
            {
                imageInfos.Add(new ImageInfo
                {
                    Name = Guid.NewGuid().ToString() + ".jpg",
                    Url = url
                });
            }
            return imageInfos;
        }
    }
}
