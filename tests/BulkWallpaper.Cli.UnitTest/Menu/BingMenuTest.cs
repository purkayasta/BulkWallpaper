using BulkImageDownloader.Cli.Menu;
using Moq;
using Xunit;

namespace BulkImageDownloader.Cli.Console.Menu
{
	public class BingMenuTest
	{
		private readonly Mock<BingMenu> _mockSut;
		private readonly BingMenu _sut;
		public BingMenuTest()
		{
			_mockSut = new Mock<BingMenu>();
			_sut = _mockSut.Object;
		}

		[Fact]
		public void BuildUrl_ShouldReturnTwoUrl_WhenProvidedMaxDownloadCount()
		{
			var url = "HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US";
			int selectedDownloadCount = 14;
			_mockSut.Setup(x => x.DownloadableImageCount).Returns(selectedDownloadCount);
			_mockSut.Setup(x => x.Url).Returns(url);

			var expectedUrls = _sut.BuildUrl();

			Assert.Equal(expectedUrls.Length, 2);
		}
	}
}
