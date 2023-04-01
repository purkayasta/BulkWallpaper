using BulkWallpaper.CLI.Services;
using ConsoleTools;

namespace BulkImageDownloader.Cli.Menu
{
    internal class BulkWallpaperMenu
	{
		internal static async Task<ConsoleMenu> BuildAsync()
		{
			await Console.Out.WriteLineAsync("Preparing Menu");
			return new ConsoleMenu
			{
				{ "Bing", async () => await OrchestrationService.ActivateBing() },
				{ "Unsplash",async () => await OrchestrationService.ActivateUnsplash() }
			};
		}
	}
}
