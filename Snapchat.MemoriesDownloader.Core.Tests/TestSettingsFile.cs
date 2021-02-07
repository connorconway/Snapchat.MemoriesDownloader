using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Snapchat.MemoriesDownloader.Core.Tests
{
    public static class TestSettingsFile
    {
        private static string FileName => "local-appsettings.json";
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
                                                                   .SetBasePath(TestContext.CurrentContext.TestDirectory)
                                                                   .AddJsonFile(FileName)
                                                                   .Build();

        public static string TestMemoryId => Configuration["TestMemoryHistoryId"];
    }
}