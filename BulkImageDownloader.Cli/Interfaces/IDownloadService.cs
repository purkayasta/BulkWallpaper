using System.Threading.Tasks;
using BulkImageDownloader.Cli.ViewModels;

namespace BulkImageDownloader.Cli.Interfaces
{
	interface IDownloadService
	{
		public Task InitiateDownloadAsync(WallpaperProviderBuilder wallpaperProvider);
	}
}
