using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Snapchat.MemoriesDownloader.Core
{
    public class Memory
    {
        private readonly string _downloadUrl;
        private readonly string _date;
        private readonly string _mediaType;

        public Memory(string downloadUrl, string date, string mediaType)
        {
            _downloadUrl = downloadUrl;
            _date = date;
            _mediaType = mediaType;
        }

        public async Task<HttpResponseMessage> Download()
        {
            var Client = new HttpClient();
            var parts = _downloadUrl.Split("?");
            var getRequest = HttpRequestBuilder.PostRequest(new Uri(parts[0]));
            getRequest.Content = new StringContent(parts[1], Encoding.UTF8, "application/x-www-form-urlencoded");
            return await Client.SendAsync(getRequest);
        }
    }
}