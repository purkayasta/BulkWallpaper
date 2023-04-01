using System.Text;
using BulkWallpaper.CLI.Menu;
using BulkWallpaper.Utils;

Console.OutputEncoding = Encoding.Unicode;


Console.WriteLine(Credits.WelcomeAnsiText);

try
{
	await MenuViewer.ShowAsync();
}
catch (Exception)
{
	Console.WriteLine(Credits.NetworkError);
	Console.WriteLine(Credits.SorryText);
}

Console.WriteLine(Credits.GoodByeAnsiText);
Console.WriteLine("To know more about visit: " + ContactDeveloper.RepositoryLink);