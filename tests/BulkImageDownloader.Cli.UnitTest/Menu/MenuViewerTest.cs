using BulkImageDownloader.Cli.Menu;
using Xunit;

namespace BulkImageDownloader.Cli.Console.Menu
{
	public class MenuViewerTest
	{
		[Fact]
		public void DetectWallpaperProvider_ShouldDetectBing_WhenProvidedBing()
		{
			var wallpaperSelector = "bing";
			var actualAnswer = ClientEnum.Bing;

			var expectedAnswer = MenuViewer.DetectWallpaperProvider(wallpaperSelector);

			Assert.Equal(expectedAnswer, actualAnswer);
		}

		[Fact]
		public void DetectWallpaperProvider_ShouldDetectUnsplash_WhenProvidedInvalidOrDefault()
		{
			var wallpaperSelector = "";
			var actualAnswer = ClientEnum.Unsplash;

			var expectedAnswer = MenuViewer.DetectWallpaperProvider(wallpaperSelector);

			Assert.Equal(expectedAnswer, actualAnswer);
		}
	}
}
