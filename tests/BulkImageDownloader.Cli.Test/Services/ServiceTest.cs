using AutoFixture;
using BulkImageDownloader.Cli.Interfaces;
using BulkImageDownloader.Cli.Services;
using BulkImageDownloader.Cli.ViewModels;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BulkImageDownloader.Cli.Test.Services
{
    public class ServiceTest
    {
        private readonly Fixture _autoFixture;
        private Service _sut;
        private Mock<IDownloadService> _downloadServiceMock;
        private readonly IDownloadService _downloadService;

        public ServiceTest()
        {
            _autoFixture = new Fixture();
            _downloadServiceMock = new Mock<IDownloadService>();
            _downloadService = _downloadServiceMock.Object;
        }

        [Fact]
        public void StartDownloadService_ShouldStartUnsplashService_WhenUnsplashProvided()
        {
            var wallpaperModel = _autoFixture.Create<WallpaperModel>();
            Task t = Task.FromResult(true);

            bool isServiceStarted = true;
            _downloadServiceMock.Setup(x => x.InitiateDownloadAsync(wallpaperModel)).Returns(t);
            Service.StartDownloadService(_downloadService, wallpaperModel).GetAwaiter().GetResult();

            Assert.Equal(t.IsCompleted, isServiceStarted);
        }
    }
}
