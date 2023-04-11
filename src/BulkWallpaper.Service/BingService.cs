using BingWallpaper.Contracts;
using BingWallpaper.Installer;
using BulkWallpaper.Utils;

namespace BulkWallpaper.Service
{
    public static class BingService
    {
        private static readonly IBingWallpaperService? _wallpaperService = BingWallpaperInstaller.CreateService();

        public static void Download(int download, string location)
        {
            var bingWallpaperSource = _wallpaperService!.GetDailyWallpaperInfo(download);

            if (bingWallpaperSource == null || !bingWallpaperSource.Any())
            {
                Console.WriteLine("Cannot find any image description. Create a github issue please.");
                Console.WriteLine("Github Issue: " + ContactDeveloper.GithubIssueBoardLink);
                Console.WriteLine(Credits.SorryText);
            }

            foreach (var image in bingWallpaperSource!)
            {
                try
                {
                    Console.WriteLine("Title: " + image?.Title);
                    Console.WriteLine("Copyright: " + image?.CopyRight);
                    _wallpaperService.Download(image?.Url!, image?.Title!, "png", location);
                    Console.WriteLine("\n");
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong when downloading from bing!");
                    Console.WriteLine("Create a issue in the github " + ContactDeveloper.GithubIssueBoardLink);
                }
            }
        }
    }
}
