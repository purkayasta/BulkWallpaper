using System.Text;

namespace BulkWallpaper.Service
{
	public class UnsplashService
	{
		private const string _baseUrl = "https://source.unsplash.com";
		private const string _picResolution = "1920*1080";
		private readonly static DownloadService _downloadService = new(new HttpClient());

		public static async Task DownloadAsync(int downloadCount, string localPath, string? tags, bool isFeatured)
		{
			var url = GetUnsplashUrl(tags, isFeatured);

			for (int i = 0; i < downloadCount; i++)
			{
				await _downloadService.DownloadAsync(
					url: url,
					localPath: localPath,
					imageName: Guid.NewGuid().ToString(),
					imageExtension: "png");

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
