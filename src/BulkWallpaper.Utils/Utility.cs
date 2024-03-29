﻿namespace BulkWallpaper.Utils
{
    public static class Utility
    {
        public static int InputAndValidateDownloadCount(int minimum, int maximum)
        {
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("Invalid Input! again..");
                InputAndValidateDownloadCount(minimum, maximum);
            }

            if (number < minimum || number > maximum)
            {
                Console.WriteLine("Invalid input! please input again..");
                InputAndValidateDownloadCount(minimum, maximum);
            }
            return number;
        }

        public static DirectoryInfo? InputAndValidateLocation(string? prefix = null)
        {
            string? location = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(location) || string.IsNullOrWhiteSpace(location))
            {
                Console.WriteLine("Invalid Input! Input again..");
                InputAndValidateLocation(prefix);
            }

            try
            {
                var directoryInfo = Directory.CreateDirectory(location! + "/" + prefix);
                return directoryInfo;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid directory location! Input again..");
                InputAndValidateLocation(prefix);
            }

            return null;
        }

        public static string InputAndValidatedStringTagInput()
        {
            Console.WriteLine("Add comma separated tags in a line.");
            string? tagInput = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(tagInput) || string.IsNullOrWhiteSpace(tagInput))
            {
                Console.WriteLine("No tag entered");
                InputAndValidatedStringTagInput();
            }
            return tagInput!;
        }

        public static bool InputAndValidateYesNoAnswer()
        {
            Console.WriteLine("Input [Y]es/[N]o");
            string? strInput = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(strInput) || string.IsNullOrWhiteSpace(strInput))
            {
                Console.WriteLine("Incorrect Input");
                InputAndValidatedStringTagInput();
            }

            if (strInput!.ToLower().Equals("y", StringComparison.Ordinal) || strInput.ToLower().Equals("yes", StringComparison.Ordinal))
                return true;
            if (strInput!.ToLower().Equals("n", StringComparison.Ordinal) || strInput.Equals("no", StringComparison.Ordinal))
                return false;

            throw new Exception("Invalid User Input.");
        }
    }
}