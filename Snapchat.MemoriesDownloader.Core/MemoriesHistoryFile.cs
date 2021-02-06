using System.IO;
using Newtonsoft.Json;

namespace Snapchat.MemoriesDownloader.Core
{
    public class MemoriesHistoryFile
    {
        public static string Name => "memories_history.json";

        private readonly string _filePath;

        public MemoriesHistoryFile(string filePath) => _filePath = filePath;

        public MemoriesHistoryDto Memories => JsonConvert.DeserializeObject<MemoriesHistoryDto>(File.ReadAllText(_filePath));
    }
}
