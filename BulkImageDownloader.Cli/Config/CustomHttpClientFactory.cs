using System;
using System.Net.Http;

namespace BulkImageDownloader.Cli.Config
{
	public sealed class CustomHttpClientFactory : IHttpClientFactory
    {
        private static readonly Lazy<HttpClient> _httpClient = new(() => new HttpClient());
        public HttpClient CreateClient(string name) => _httpClient.Value;
    }
}
