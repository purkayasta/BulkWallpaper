using System.Threading.Tasks;
using BulkImageDownloader.Cli.Helper;

namespace BulkImageDownloader.Cli.Services
{
	interface IDownloadService
	{
		public Task InitiateDownloadAsync(WallpaperProviderBuilder wallpaperProvider);
	}
}
