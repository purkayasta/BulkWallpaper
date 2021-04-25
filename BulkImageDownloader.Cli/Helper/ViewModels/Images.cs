using System.Text.Json.Serialization;

namespace BulkImageDownloader.Cli.Helper.ViewModels
{
	public record Images
	{
		[JsonPropertyName("url")]
		public string Url { get; init; }

		[JsonPropertyName("title")]
		public string Name { get; init; }

	}
}
