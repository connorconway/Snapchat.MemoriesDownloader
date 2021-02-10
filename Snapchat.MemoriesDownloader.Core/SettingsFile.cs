using Microsoft.Extensions.Configuration;

namespace Snapchat.MemoriesDownloader.Core
{
    public static class SettingsFile
    {
        private static string FileName => "appsettings.json";
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
                                                                   .AddJsonFile(FileName)
                                                                   .Build();

        public static string BaseSnapchatRouteUrl => Configuration["BaseSnapchatRouteUrl"];
    }
}