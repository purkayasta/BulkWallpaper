using BulkImageDownloader.Cli.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli.Interfaces
{
    public interface IPexelService : IDownloadService
	{
		Task<List<ImageInfo>> GetCurratedImagesAsync(int count);
		Task<List<ImageInfo>> SearchPhotosByNameAsync(string name, int count);
	}
}
