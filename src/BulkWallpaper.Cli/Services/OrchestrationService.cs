using BulkWallpaper.CLI.Menu;
using BulkWallpaper.Service;
using BulkWallpaper.Utils;

namespace BulkWallpaper.CLI.Services
{
    internal sealed class OrchestrationService
    {
        internal void Run(MenuOption menuOption)
        {
            switch (menuOption)
            {
                case MenuOption.Exit:
                    break;
                case MenuOption.Bing:
                    ActivateBing();
                    break;
                case MenuOption.Unsplash:
                    ActivateUnsplash();
                    break;
                default:
                    throw new ArgumentException("Invalid Option");
            }
        }
        internal static void ActivateBing()
        {
            var bingItem = BingMenu.ShowMenu();

            if (bingItem.Item2 == null || bingItem.Item1 < 1)
            {
                Console.WriteLine("Critical Error! Create a github issue");
                Console.WriteLine("Github Issue: " + ContactDeveloper.GithubIssueBoardLink);
                Console.WriteLine("Repository: " + ContactDeveloper.RepositoryLink);
            }
            new BingService().Download(bingItem.Item1, bingItem.Item2!.FullName);
        }

        internal static void ActivateUnsplash()
        {
            var menuItem = UnsplashMenu.ShowMenu();

            if (menuItem.Item2 == null || menuItem.Item1 < 1)
            {
                Console.WriteLine("Critical Error! Create a github issue");
                Console.WriteLine("Github Issue: " + ContactDeveloper.GithubIssueBoardLink);
                Console.WriteLine("Repository: " + ContactDeveloper.RepositoryLink);
            }
            new UnsplashService().Download(menuItem.Item1, menuItem.Item2!.FullName, menuItem.Item3, menuItem.Item4);
        }
    }
}
