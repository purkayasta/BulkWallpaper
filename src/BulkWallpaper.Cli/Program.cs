﻿using System.Text;
using BulkWallpaper.CLI.Menu;
using BulkWallpaper.CLI.Services;
using BulkWallpaper.Utils;

Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;

Console.WriteLine(Credits.WelcomeAnsiText);

try
{
    var menuOption = MenuViewer.Show();
    if (menuOption is null)
    {
        Console.WriteLine(Credits.SorryText);
        Console.WriteLine("Please file a issue here: " + ContactDeveloper.GithubIssueBoardLink);
        return;
    }
    OrchestrationService.Run(menuOption.Value);

}
catch (Exception ex)
{
    Console.WriteLine(ex);
    Console.WriteLine("\n");
    Console.WriteLine(Credits.TryAgainText);
}
Console.WriteLine("\n");
Console.WriteLine(Credits.ThankYouText);
Console.WriteLine("\n");
Console.WriteLine("To know more about visit: " + ContactDeveloper.RepositoryLink);