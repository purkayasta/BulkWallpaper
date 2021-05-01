using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BulkImageDownloader.Cli.ViewModels;
using BulkImageDownloader.Cli.Interfaces;
using BulkImageDownloader.Cli.Helper;

namespace BulkImageDownloader.Cli.Services
{
    public class BingService : BaseService, IBingService
    {
        public BingService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, ClientEnum.Bing)
        {
        }
        public async Task InitiateDownloadAsync(WallpaperModel wallpaperProvider)
        {
            Console.WriteLine("⏬ Downloading .... ");

            var directory = $"{wallpaperProvider.DirectoryLocation}/Bing/";

            Directory.CreateDirectory(directory);

            int totalCount = wallpaperProvider.BingApiResponses.Count;
            int progress = 0;

            foreach (var item in wallpaperProvider.BingApiResponses)
            {
                var responses = await GetContentAsync(item.Url);
                await SaveAsync(responses, $"{directory}/{item.Name.Trim().RemoveSpecialCharacters()}.png");

                progress++;
                Console.Write($"\r {progress} | {totalCount}");
            }

            Console.WriteLine("");
            Console.WriteLine("⏬ Downloaded ....  ✔ 💹");
        }
    }
}
