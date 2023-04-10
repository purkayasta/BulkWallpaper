using BulkWallpaper.CLI.Services;
using BulkWallpaper.Utils;

namespace BulkWallpaper.CLI.Menu
{
    internal static class MenuViewer
    {
        private static bool isTaskDone = false;
        internal static async Task ShowAsync()
        {
            Console.WriteLine("Preparing Menu");

            var options = GetMenuOptions();

            while (!isTaskDone)
            {
                for (int i = 0; i < options.Count; i++)
                    Console.WriteLine($"{i + 1}. {options[i + 1.ToMenuOption()]}");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid Option");
                    continue;
                }

                try
                {
                    var menuOption = choice.ToMenuOption();
                    await OrchestrationService.Activate(menuOption);
                    isTaskDone = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Option");
                    continue;
                }
            }
        }
        private static Dictionary<MenuOption, string> GetMenuOptions()
        {
            return new Dictionary<MenuOption, string>
            {
                { MenuOption.Bing, MenuOption.Bing.ToText() },
                { MenuOption.Unsplash, MenuOption.Unsplash.ToText() },
                { MenuOption.Pexel, MenuOption.Pexel.ToText() },
                { MenuOption.Exit, MenuOption.Exit.ToText() }
            };
        }
    }
}
