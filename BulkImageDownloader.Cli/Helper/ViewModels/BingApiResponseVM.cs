using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BulkImageDownloader.Cli.Helper.ViewModels
{
	public record BingApiResponseVM
	{
		[JsonPropertyName("images")]
		public IReadOnlyList<Images> Images { get; init; }
	}
}
