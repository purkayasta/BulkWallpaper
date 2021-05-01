using System.Threading.Tasks;
using BulkImageDownloader.Cli.ViewModels;

namespace BulkImageDownloader.Cli.Interfaces
{
	public interface IDownloadService
	{
		public Task InitiateDownloadAsync(WallpaperModel wallpaperProvider);
	}
}
