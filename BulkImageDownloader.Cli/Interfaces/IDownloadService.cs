using System.Threading.Tasks;
using BulkImageDownloader.Cli.Helper.ViewModels;

namespace BulkImageDownloader.Cli.Interfaces
{
	interface IDownloadService
	{
		public Task InitiateDownloadAsync(WallpaperProviderBuilder wallpaperProvider);
	}
}
