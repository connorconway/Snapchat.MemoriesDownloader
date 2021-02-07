using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Snapchat.MemoriesDownloader.Core
{
    public class Memory : IEquatable<Memory>
    {
        private readonly SnapchatApiClient _client = new SnapchatApiClient();
        private readonly string _date;
        private readonly string _mediaType;
        private readonly string _id;
        private readonly DateTime _dateAsDate;

        public Memory(string id, string date, string mediaType)
        {
            _id = id;
            _date = date;
            _mediaType = mediaType;
            _dateAsDate = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss UTC", CultureInfo.InvariantCulture);
        }

        public async Task<string> SaveAsync()
        {
            var fullMonthName = _dateAsDate.ToString("MMMM");
            var fullDayName = _dateAsDate.ToString("dddd");

            var directoryPath = Path.Combine(_dateAsDate.Year.ToString(), $"{_dateAsDate.Month:00} ({fullMonthName})", $"{_dateAsDate.Day:00} ({fullDayName})");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var hashesOfFilesInDirectory = Directory.GetFiles(directoryPath).Select(Sha256.CalculateFrom);

            switch (_mediaType)
            {
                case "VIDEO":
                    var path = Path.Combine(directoryPath, $"{_date.Replace(':', '-')}.mp4");
                    if (File.Exists(path))
                        return path;
                    break;
                case "PHOTO":
                    path = Path.Combine(directoryPath, $"{_date.Replace(':', '-')}.jpg");
                    if (File.Exists(path))
                        return path;
                    break;
            }

            var downloadLink = await _client.GetDownloadLinkAsync(_id);
            var byteStream = await _client.GetFileByteArrayAsync(downloadLink);

            if (hashesOfFilesInDirectory.Contains(Sha256.CalculateFrom(byteStream)))
            {
                Console.WriteLine("Image already downloaded. Skipping.");
                return string.Empty;
            }

            switch (_mediaType)
            {
                case "VIDEO":
                    var path = Path.Combine(directoryPath, $"{_date.Replace(':', '-')}.mp4");
                    Console.WriteLine($"downloading {path}");
                    await File.WriteAllBytesAsync(path, byteStream);
                    return path;
                case "PHOTO":
                    path = Path.Combine(directoryPath, $"{_date.Replace(':', '-')}.jpg");
                    Console.WriteLine($"downloading {path}");
                    await File.WriteAllBytesAsync(path, byteStream);
                    return path;
            }

            throw new Exception("Invalid media type");
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
    }
}