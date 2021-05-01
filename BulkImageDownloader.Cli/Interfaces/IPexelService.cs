using System.Collections;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli.Interfaces
{
	public interface IPexelService : IDownloadService
	{
		Task<ArrayList> GetCurratedImagesAsync(int count);
		Task<ArrayList> SearchPhotosByNameAsync(string name, int count);
	}
}
