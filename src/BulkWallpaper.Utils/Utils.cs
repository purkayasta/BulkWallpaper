namespace BulkWallpaper.Utils
{
	public partial class Utils
	{
		public static int InputAndValidateDownloadCount(int minimum, int maximum)
		{
			if (!int.TryParse(Console.ReadLine(), out int number))
				InputAndValidateDownloadCount(minimum, maximum);

			if (number < minimum || number > maximum)
			{
				Console.WriteLine("Invalid input! please input again..");
				InputAndValidateDownloadCount(minimum, maximum);
			}
			return number;
		}

		public static DirectoryInfo? InputAndValidateLocation()
		{
			string? location = Console.ReadLine()?.Trim();

			if (string.IsNullOrEmpty(location) || string.IsNullOrWhiteSpace(location))
			{
				Console.WriteLine("Invalid Input! Input again..");
				InputAndValidateLocation();
			}

			try
			{
				var directoryInfo = Directory.CreateDirectory(location!);
				return directoryInfo;
			}
			catch (Exception)
			{
				Console.WriteLine("Invalid directory location! Input again..");
				InputAndValidateLocation();
			}

			return null;
		}
	}
}