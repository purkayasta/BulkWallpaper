using BulkImageDownloader.Cli.ViewModels;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli.Interfaces
{
    public interface IDownloadService
	{
		public Task InitiateDownloadAsync(WallpaperModel wallpaperProvider);
	}
}
