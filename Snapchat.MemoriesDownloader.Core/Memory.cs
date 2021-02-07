using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Snapchat.MemoriesDownloader.Core
{
    public class Memory
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _date;
        private readonly string _mediaType;
        private readonly string _id;

        public Memory(string id, string date, string mediaType)
        {
            _id = id;
            _date = date;
            _mediaType = mediaType;
        }

        public async Task<string> DownloadLink(Uri route)
        {
            var postRequest = HttpRequestBuilder.PostRequest(route)
                                                .WithContent(_id)
                                                .WithEncoding(Encoding.UTF8)
                                                .WithMediaType("application/x-www-form-urlencoded")
                                                .Build();

            var result = await _client.SendAsync(postRequest);
            var downloadLink = await result.Content.ReadAsStringAsync();
            return downloadLink;
        }

        public async Task<bool> Download(Uri route)
        {
            var getRequest = HttpRequestBuilder.GetRequest(route)
                                               .Build();

            var result = await _client.SendAsync(getRequest);
            var con = result.Content;
            switch (_mediaType)
            {
                case "VIDEO":
                    await File.WriteAllBytesAsync($"{_date}.mp4", await con.ReadAsByteArrayAsync());
                    break;
                case "IMAGE":
                    await File.WriteAllBytesAsync($"{_date}.jpg", await con.ReadAsByteArrayAsync());
                    break;
            }
            return result.IsSuccessStatusCode;
        }
    }
}