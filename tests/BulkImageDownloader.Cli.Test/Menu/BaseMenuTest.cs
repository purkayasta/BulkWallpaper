using BulkImageDownloader.Cli.Menu;
using Moq;
using System.IO;
using Xunit;

namespace BulkImageDownloader.Cli.Test.Menu
{
    public class BaseMenuTest
	{
		private readonly Mock<BaseMenu> _mockSut;
		private readonly BaseMenu _sut;

		public BaseMenuTest()
		{
			_mockSut = new Mock<BaseMenu>(ClientEnum.Bing);
			_sut = _mockSut.Object;
		}

		[Fact]
		public void WallpaperCountSelector_ShouldReturnTheDefault_WhenProvidedNone()
		{
			string defaultCount = "";

			_mockSut.Setup(x => x.DownloadableImageCount).Returns(10);
			_mockSut.Setup(x => x.MaxDownloadLimit).Returns(14);

			_mockSut.Setup(x => x.GetUserInput()).Returns(defaultCount);

			int actualValue = 10;

			_sut.WallpaperCountSelector();

			Assert.Equal(_sut.DownloadableImageCount, actualValue);
		}

		[Fact]
		public void WallpaperCountSelector_ShouldReturn_WhenProvidedByUser()
		{
			int userDefault = 9;
			_mockSut.Setup(x => x.DownloadableImageCount).Returns(userDefault);
			_mockSut.Setup(x => x.MaxDownloadLimit).Returns(14);

			_mockSut.Setup(x => x.GetUserInput()).Returns(userDefault.ToString());

			_sut.WallpaperCountSelector();

			Assert.Equal(_sut.DownloadableImageCount, userDefault);
		}

		[Fact]
		public void WallpaperTypeSelector_ShouldReturnDefault_WhenProvidedNone()
		{
			string userDefault = "4k wallpapers";
			_mockSut.Setup(x => x.Tags).Returns(userDefault);

			_sut.WallpaperTypeSelector();

			Assert.Equal(_sut.Tags, userDefault);
		}

		[Fact]
		public void WallpaperTypeSelector_ShouldReturnProvided_WhenProvidedByUser()
		{
			string userDefault = "4k wallpapers";
			string userInput = "Tree";

			_mockSut.Setup(x => x.Tags).Returns(userDefault);
			_mockSut.Setup(x => x.GetUserInput()).Returns(userInput);
			_mockSut.Setup(x => x.Tags).Returns(userInput);


			_sut.WallpaperTypeSelector();

			Assert.Equal(_sut.Tags, userInput);
		}

		[Fact]
		public void DirectoryLocationSelector_ShouldReturnCurrentDirectory_WhenProvidedNone()
		{
			string userInput = "";
			string actualValue = "C:/Unsplash";

			_mockSut.Setup(x => x.GetUserInput()).Returns(userInput);
			_mockSut.Setup(x => x.DownloadedDirectory).Returns(actualValue);

			_sut.DirectoryLocationSelector();

			Assert.Equal(_sut.DownloadedDirectory, actualValue);
		}

		[Fact]
		public void DirectoryLocationSelector_ShouldReturnProvided_WhenProvidedByUser()
		{
			string defaultDirectory = "F:";
			string userInput = "F:";
			string actualValue = "F://Unsplash";

			DirectoryInfo directoryInfo = new(defaultDirectory + "/" + "Unsplash");
			_mockSut.Setup(x => x.IsDirectoryExist(userInput)).Returns(true);
			_mockSut.Setup(x => x.CreateDirectory(defaultDirectory)).Returns(directoryInfo);
			_mockSut.Setup(x => x.DownloadedDirectory).Returns(actualValue);

			_sut.DirectoryLocationSelector();

			Assert.Equal(_sut.DownloadedDirectory, actualValue);
		}
	}
}
