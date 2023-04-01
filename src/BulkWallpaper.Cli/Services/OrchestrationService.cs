using BulkWallpaper.CLI.Menu;
using BulkWallpaper.Service;
using BulkWallpaper.Utils;

namespace BulkWallpaper.CLI.Services
{
	internal class OrchestrationService
	{
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
