using BulkImageDownloader.Cli.ViewModels;
using Refit;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli.Interfaces
{
    public interface IBingApi
	{
		[Get("/{urlPostFix}")]
		public Task<BingApiResponseVM> GetResponseAsync(string urlPostFix);
	}
}
