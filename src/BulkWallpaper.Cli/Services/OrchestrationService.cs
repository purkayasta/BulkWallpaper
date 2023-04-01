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
			await Console.Out.WriteLineAsync();
		}
	}
}
