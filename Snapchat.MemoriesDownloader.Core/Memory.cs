using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Snapchat.MemoriesDownloader.Core
{
    public class Memory : IEquatable<Memory>
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

        public async Task<string> SaveAsync(Uri route)
        {
            var downloadedFileInBytes = await DownloadAsync(route);
            switch (_mediaType)
            {
                case "VIDEO":
                    var path = $"{_date.Replace(':', '-')}.mp4";
                    await File.WriteAllBytesAsync(path, downloadedFileInBytes);
                    return path;
                case "PHOTO":
                    path = $"{_date.Replace(':', '-')}.jpg";
                    await File.WriteAllBytesAsync(path, downloadedFileInBytes);
                    return path;
            }

            throw new Exception("Invalid media type");
        }

        private async Task<string> DownloadLinkAsync(Uri route)
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

        private async Task<byte[]> DownloadAsync(Uri route)
        {
            var downloadLink = await DownloadLinkAsync(route);

            var getRequest = HttpRequestBuilder.GetRequest(new Uri(downloadLink))
                                               .Build();

            var result = await _client.SendAsync(getRequest);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsByteArrayAsync();
            
            throw new Exception("API call failed");
        }

        public bool Equals(Memory other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _date == other._date && _mediaType == other._mediaType && _id == other._id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Memory) obj);
        }

        public override int GetHashCode() => HashCode.Combine(_date, _mediaType, _id);
    }
}