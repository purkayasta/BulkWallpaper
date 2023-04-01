namespace BulkWallpaper.Utils
{
	public enum MenuOption
	{
		Unsplash = 1,
		Bing = 2,
		Pexel = 3,
		Exit = 4
	}

	public static class EnumExtensions
	{
		public static MenuOption ToMenuOption(this int number)
			=> (MenuOption)number;
		public static int ToNumber(this MenuOption value)
			=> (int)value;
		public static string ToText(this MenuOption value)
		{
			return value switch
			{
				MenuOption.Bing => "Bing",
				MenuOption.Unsplash => "Unsplash",
				MenuOption.Pexel => "Pexels",
				MenuOption.Exit => "Exit",
				_ => string.Empty
			};
		}
	}
}
