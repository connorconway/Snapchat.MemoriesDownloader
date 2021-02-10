using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Snapchat.MemoriesDownloader.Core
{
    public class Memory : IEquatable<Memory>, IDisposable
    {
        private readonly SnapchatApiClient _client = new SnapchatApiClient();
        private readonly string _date;
        private readonly string _mediaType;
        private readonly string _id;
        private readonly DateTime _dateAsDate;
        private byte[] _byteStream;
        private string _downloadLink;
        private string _filePath;

        public Memory(string id, string date, string mediaType)
        {
            _id = id;
            _date = date;
            _mediaType = mediaType;
            _dateAsDate = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss UTC", CultureInfo.InvariantCulture);
        }

        public async Task<string> HashAsync()
        {
            if (_mediaType.Equals("VIDEO"))
                return Guid.NewGuid().ToString();
            _downloadLink = _downloadLink ??= await _client.GetDownloadLinkAsync(_id);
            _byteStream = _byteStream ??= await _client.GetFileByteArrayAsync(_downloadLink);
            return Md5.CalculateFrom(_byteStream);
        }

        public bool Exists()
        {
            var fullMonthName = _dateAsDate.ToString("MMMM");
            var fullDayName = _dateAsDate.ToString("dddd");

            var directoryPath = Path.Combine(_dateAsDate.Year.ToString(), $"{_dateAsDate.Month:00} ({fullMonthName})", $"{_dateAsDate.Day:00} ({fullDayName})");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            _filePath = _filePath ??= _mediaType switch
            {
                "VIDEO" => Path.Combine(directoryPath, $"{_date.Replace(':', '-')}.mp4"),
                "PHOTO" => Path.Combine(directoryPath, $"{_date.Replace(':', '-')}.jpg"),
                _ => string.Empty
            };

            if (string.IsNullOrEmpty(_filePath))
                Console.WriteLine($"Error - Invalid Media Type ({_mediaType}) for memory with Id: {_id}.");

            if (File.Exists(_filePath))
            {
                Console.WriteLine($"Skipping - File already exists '{_filePath}'.");
                return true; 
            }

            return false;
        }

        public async Task<string> SaveAsync()
        {
            _downloadLink = _downloadLink ??= await _client.GetDownloadLinkAsync(_id);
            _byteStream = _byteStream ??= await _client.GetFileByteArrayAsync(_downloadLink);
            
            await File.WriteAllBytesAsync(_filePath, _byteStream);
            Console.WriteLine($"Success - Memory saved: {_filePath}");

            return _filePath;
        }

        public bool Equals(Memory other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Memory) obj);
        }

        public override int GetHashCode() => HashCode.Combine(_id);

        public void Dispose() => _byteStream = null;
    }
}