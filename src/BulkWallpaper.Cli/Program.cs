using System.Text;
using BulkImageDownloader.Cli.Menu;
using BulkWallpaper.Utils;

Console.OutputEncoding = Encoding.Unicode;

Console.WriteLine(Credits.WelcomeAnsiText);

try
{
	var menu = await BulkWallpaperMenu.BuildAsync();
	await menu.ShowAsync();
}
catch (Exception)
{
	Console.WriteLine(Credits.NetworkError);
	Console.WriteLine(Credits.SorryText);
}
