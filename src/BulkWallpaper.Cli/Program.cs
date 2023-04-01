using System.Text;
using BulkWallpaper.CLI.Menu;
using BulkWallpaper.Utils;

//Console.OutputEncoding = Encoding.Unicode;
Console.WriteLine(Credits.WelcomeAnsiText);

try
{
	await MenuViewer.ShowAsync();
}
catch (Exception)
{
	Console.WriteLine("\n");
	Console.WriteLine(Credits.TryAgainText);
}
Console.WriteLine("\n");
Console.WriteLine(Credits.ThankYouText);
Console.WriteLine("\n");
Console.WriteLine("To know more about visit: " + ContactDeveloper.RepositoryLink);