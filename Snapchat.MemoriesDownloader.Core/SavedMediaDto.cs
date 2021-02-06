using Newtonsoft.Json;

namespace Snapchat.MemoriesDownloader.Core
{
    public class SavedMediaDto
    {
        [JsonProperty("Date")]
        public string Date { get; set; }

        [JsonProperty("Media Type")]
        public string MediaType { get;set; }

        [JsonProperty("Download Link")]
        public string DownloadLink { get; set; }
    }
}