using BulkWallpaper.CLI.Menu;
using BulkWallpaper.Service;
using BulkWallpaper.Utils;

namespace BulkWallpaper.CLI.Services
{
	internal class OrchestrationService
	{
		internal static async Task Activate(MenuOption menuOption)
		{
			switch (menuOption)
			{
				case MenuOption.Bing:
					await ActivateBing();
					break;
				case MenuOption.Unsplash:
					await ActivateUnsplash();
					break;
				default:
					throw new ArgumentException("Invalid Option");
			}
		}
		internal static async Task ActivateBing()
		{
			var bingItem = BingMenu.ShowMenu();

			if (bingItem.Item2 == null || bingItem.Item1 < 1)
			{
				Console.WriteLine("Critical Error! Create a github issue");
				Console.WriteLine("Github Issue: " + ContactDeveloper.GithubIssueBoardLink);
				Console.WriteLine("Repository: " + ContactDeveloper.RepositoryLink);
			}
			await BingService.DownloadAsync(bingItem.Item1, bingItem.Item2!.FullName);
		}

		internal static async Task ActivateUnsplash()
		{
			var menuItem = UnsplashMenu.ShowMenu();

			if (menuItem.Item2 == null || menuItem.Item1 < 1)
			{
				Console.WriteLine("Critical Error! Create a github issue");
				Console.WriteLine("Github Issue: " + ContactDeveloper.GithubIssueBoardLink);
				Console.WriteLine("Repository: " + ContactDeveloper.RepositoryLink);
			}
			await UnsplashService.DownloadAsync(menuItem.Item1, menuItem.Item2!.FullName, menuItem.Item3, menuItem.Item4);
		}
	}
}
