using BulkWallpaper.Utils;

namespace BulkImageDownloader.Cli.Menu
{
    internal class BingMenu
	{
		internal static (int, DirectoryInfo?) ShowMenu()
		{
			Console.WriteLine("How many days daily Wallpaper you want to download 📷 ? (max: 14)");
			int userImageNumber = Utils.InputAndValidateDownloadCount(1, 15);

			Console.WriteLine("Where do you want to store the wallpaper? 📂 ");
			DirectoryInfo? location = Utils.InputAndValidateLocation();

			return (userImageNumber, location);
		}
	}
}
