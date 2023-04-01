using ConsoleTools;

namespace BulkImageDownloader.Cli.Menu
{
    internal class BulkWallpaperMenu
    {
        internal static ConsoleMenu Build()
        {
            return new ConsoleMenu
            {
                //{ "Bing", () => new BingService() },
                //{ "Unsplash", () => new UnsplashService() }
            };
        }
    }
}
