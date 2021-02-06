using Newtonsoft.Json;

namespace Snapchat.MemoriesDownloader.Core
{
    public class MemoriesHistoryDto
    {
        [JsonProperty("Saved Media")]
        public SavedMediaDto[] SavedMedia { get;set; }
    }
}