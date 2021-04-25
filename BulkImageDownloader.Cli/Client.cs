using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BulkImageDownloader.Cli
{
    class Client
    {
        internal static async Task CallClient(string url, string localPathWithImageName)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            using var fileStream = await response.Content.ReadAsStreamAsync();
            using var streamWriter = File.Open(localPathWithImageName, FileMode.Create);
            await fileStream.CopyToAsync(streamWriter);
        }
    }
}
