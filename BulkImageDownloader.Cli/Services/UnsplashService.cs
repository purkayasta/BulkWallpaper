using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BulkImageDownloader.Cli.ViewModels;
using BulkImageDownloader.Cli.Interfaces;

namespace BulkImageDownloader.Cli.Services
{
    public class UnsplashService : BaseService, IUnsplashService
    {
        public UnsplashService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, ClientEnum.Unsplash)
        {
        }
        public async Task InitiateDownloadAsync(WallpaperModel wallpaperModel)
        {
            Console.WriteLine("⏬ Downloading .... ");
            var imagesInfo = wallpaperModel.ImageInfos;
            var unsplashDirectory = $"{wallpaperModel.DirectoryLocation}/Unsplash/";
            Directory.CreateDirectory(unsplashDirectory);

            int progress = 0;
            int totalCount = wallpaperModel.NumberOfImages;

            foreach (var image in imagesInfo)
            {
                var responses = await GetContentAsync(image.Url);
                await SaveAsync(responses, $"{unsplashDirectory}/{image.Name}");

                progress++;
                Console.Write($"\r {progress} | {totalCount}");

                Thread.Sleep(3000);
            }

            Console.WriteLine("");
            Console.WriteLine("⏬ Downloaded ....  ✔ 💹");
        }
    }
}
