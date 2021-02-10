using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Snapchat.MemoriesDownloader.Core.Tests
{
    [TestFixture]
    public class MemoryTest
    {
        [Test]
        public async Task Download()
        {
            var memory = new Memory(TestSettingsFile.TestMemoryId, "2021-02-03 20:47:45 UTC", "VIDEO");
            var savedPath = await memory.SaveAsync();
            savedPath.Should().BeEquivalentTo(@"2021\02 (February)\03 (Wednesday)\2021-02-03 20-47-45 UTC.mp4");
        }
    }
}