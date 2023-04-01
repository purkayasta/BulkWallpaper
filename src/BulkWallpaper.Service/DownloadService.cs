namespace BulkWallpaper.Service
{
	public class DownloadService
	{
		private readonly HttpClient _httpClient;
		public DownloadService(HttpClient httpClient) => _httpClient = httpClient;

		public async Task DownloadAsync(string url, string localPath, string imageName, string imageExtension)
		{
			try
			{
				var physicalPathInfo = Directory.CreateDirectory(localPath);
				if (!physicalPathInfo.Exists)
					throw new Exception("Invalid Directory");

				var downloadablePath = physicalPathInfo.FullName + "/" + imageName + "." + imageExtension;

				using var stream = await _httpClient.GetStreamAsync(url);
				using var fileStream = File.Create(downloadablePath);
				await stream.CopyToAsync(fileStream);

				await Console.Out.WriteLineAsync($"\n File downloaded at {downloadablePath} \n");
			}
			catch (Exception exception)
			{
				await Console.Out.WriteLineAsync(exception.Message);
			}
		}
	}
}
