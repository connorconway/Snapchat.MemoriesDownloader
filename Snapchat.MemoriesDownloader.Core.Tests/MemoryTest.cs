using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Snapchat.MemoriesDownloader.Core.Tests
{
    [TestFixture]
    public class MemoryTest
    {
        [Test]
        public async Task Download()
        {
            var memory = new Memory(TestSettingsFile.TestMemoryId, "", "VIDEO");
            var d = await memory.DownloadLink(new Uri("https://app.snapchat.com/dmd/memories"));

            Assert.True(await memory.Download(new Uri(d)));
        }
    }
}