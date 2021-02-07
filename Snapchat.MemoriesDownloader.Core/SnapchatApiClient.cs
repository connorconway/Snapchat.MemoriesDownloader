using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Snapchat.MemoriesDownloader.Core
{
    public class SnapchatApiClient
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly Uri _baseRoute = new Uri(SettingsFile.BaseSnapchatRouteUrl);

        public async Task<string> GetDownloadLinkAsync(string id)
        {
            var postRequest = HttpRequestBuilder.PostRequest(_baseRoute)
                                                .WithContent(id)
                                                .WithEncoding(Encoding.UTF8)
                                                .WithMediaType("application/x-www-form-urlencoded")
                                                .Build();

            var result = await _client.SendAsync(postRequest);
            if (!result.IsSuccessStatusCode)
                throw new Exception("Unable to obtain download link");

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<byte[]> GetFileByteArrayAsync(string downloadLink)
        {
            var getRequest = HttpRequestBuilder.GetRequest(new Uri(downloadLink))
                                                .Build();

            var result = await _client.SendAsync(getRequest);
            if (!result.IsSuccessStatusCode)
                throw new Exception("Unable to download file");

            return await result.Content.ReadAsByteArrayAsync();
        }
    }
}