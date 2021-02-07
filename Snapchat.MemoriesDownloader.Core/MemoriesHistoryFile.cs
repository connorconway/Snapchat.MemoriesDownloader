using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Snapchat.MemoriesDownloader.Core
{
    public class MemoriesHistoryFile
    {
        public static string Name => "memories_history.json";

        private readonly string _filePath;
        private MemoriesHistoryDto _memoriesObject;

        public MemoriesHistoryFile(string filePath) => _filePath = filePath;

        public List<Memory> Memories()
        {
            _memoriesObject ??= JsonConvert.DeserializeObject<MemoriesHistoryDto>(File.ReadAllText(_filePath));
            var memories = _memoriesObject?.SavedMedia.Select(m => new Memory(m.DownloadLink.Split('?')[1], m.Date, m.MediaType)).ToList();
            return memories;
        }
    }
}
