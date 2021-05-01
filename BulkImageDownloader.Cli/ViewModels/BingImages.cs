using System.Text.Json.Serialization;

namespace BulkImageDownloader.Cli.ViewModels
{
	public struct BingImages
	{
		[JsonPropertyName("url")]
		public string Url { get; init; }

		[JsonPropertyName("title")]
		public string Name { get; init; }
	}
}
