﻿using System.Text;

namespace BulkWallpaper.Service
{
    public static class UnsplashService
    {
        private static readonly string _baseUrl = "https://source.unsplash.com";
        private static readonly string _picResolution = "1920x1080";
        private static readonly DownloadService _downloadService = new(new HttpClient());

        public static void Download(int downloadCount, string localPath, string? tags, bool isFeatured)
        {
            var url = GetUnsplashUrl(tags, isFeatured);
            Console.WriteLine("Alert ⚠ Downloading process will be delayed intentionally because hitting unsplash instantly will blocked the ip 💀");

            for (int i = 0; i < downloadCount; i++)
            {
                _downloadService.Download(
                    url: url,
                    localPath: localPath,
                    imageName: Guid.NewGuid().ToString(),
                    imageExtension: "png");

                // source.unsplash.com caches the ip so hitting too much at a same time won't get a new wallpaper
                // thats why there is an artificial delay to that.
                Task.Delay(2000).GetAwaiter().GetResult();
            }

        }

        private static string GetUnsplashUrl(string? tags, bool isFeatured)
        {
            StringBuilder urlBuilder = new();

            urlBuilder.Append(_baseUrl);

            if (isFeatured)
                urlBuilder.Append("/featured");

            urlBuilder.Append("/" + _picResolution);

            if (!string.IsNullOrEmpty(tags))
                urlBuilder.Append($"/?{tags}");

            return urlBuilder.ToString();
        }
    }
}
