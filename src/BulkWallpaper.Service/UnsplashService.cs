using System.Text;

namespace BulkWallpaper.Service
{
	public class UnsplashService
	{
		private const string _baseUrl = "https://source.unsplash.com";
		private const string _picResolution = "1920x1080";
		private readonly static DownloadService _downloadService = new(new HttpClient());

		public static async Task DownloadAsync(int downloadCount, string localPath, string? tags, bool isFeatured)
		{
			var url = GetUnsplashUrl(tags, isFeatured);
			await Console.Out.WriteLineAsync(url);
			await Console.Out.WriteLineAsync("Alert ⚠ Downloading process will be delayed intentionally because hitting unsplash will be blocked");

			for (int i = 0; i < downloadCount; i++)
			{
				await _downloadService.DownloadAsync(
					url: url,
					localPath: localPath,
					imageName: Guid.NewGuid().ToString(),
					imageExtension: "png");

				// source.unsplash.com caches the ip so hitting too much at a same time won't get a new wallpaper
				// thats why there is an artificial delay to that.
				await Task.Delay(2000);
			}

		}

		private static string GetUnsplashUrl(string? tags, bool isFeatured)
		{
			StringBuilder urlBuilder = new StringBuilder();

			urlBuilder.Append(_baseUrl);
			if (isFeatured)
				urlBuilder.Append("/featured");

			urlBuilder.Append("/" + _picResolution);

			if (!string.IsNullOrEmpty(tags))
				urlBuilder.Append($"/?{tags}");

			return urlBuilder.ToString();
		}
	}
}
