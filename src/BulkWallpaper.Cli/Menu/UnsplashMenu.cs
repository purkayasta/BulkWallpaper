using BulkWallpaper.Utils;

namespace BulkWallpaper.CLI.Menu
{
    internal static class UnsplashMenu
    {
        internal static (int, DirectoryInfo?, string?, bool) ShowMenu()
        {
            Console.WriteLine("How many days daily Wallpaper you want to download 📷 ? (max: 50)");
            int userImageNumber = Utility.InputAndValidateDownloadCount(1, 50);

            Console.WriteLine("Where do you want to store the wallpaper? 📂 ");
            DirectoryInfo? location = Utility.InputAndValidateLocation("Unsplash-Random");

            Console.WriteLine("Any tag? 💡");
            var tagStr = Utility.InputAndValidatedStringTagInput();

            Console.WriteLine("Do you want to download featured images? 📷");
            var isFeatured = Utility.InputAndValidateYesNoAnswer();

            return (userImageNumber, location, tagStr, isFeatured);
        }
    }
}
