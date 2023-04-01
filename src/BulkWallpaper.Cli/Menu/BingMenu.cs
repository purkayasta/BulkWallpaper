using BulkWallpaper.Utils;

namespace BulkWallpaper.CLI.Menu
{
	internal class BingMenu
	{
		internal static (int, DirectoryInfo?) ShowMenu()
		{
			Console.WriteLine("How many days daily Wallpaper you want to download 📷 ? (max: 14)");
			int userImageNumber = Utility.InputAndValidateDownloadCount(1, 15);

			Console.WriteLine("Where do you want to store the wallpaper? 📂 ");
			DirectoryInfo? location = Utility.InputAndValidateLocation();

			return (userImageNumber, location);
		}
	}
}
