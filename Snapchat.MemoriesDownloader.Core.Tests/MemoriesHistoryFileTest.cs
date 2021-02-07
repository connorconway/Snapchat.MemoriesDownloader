using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Snapchat.MemoriesDownloader.Core.Tests
{
    [TestFixture]
    public class MemoriesHistoryFileTest
    {
        [Test]
        public void Memories()
        {
            var filePath = Path.Combine("TestData", MemoriesHistoryFile.Name);
            var expectedMemories = new List<Memory>
            {
                    new Memory("id1", "2021-02-03 20:47:45 UTC", "VIDEO"),
                    new Memory("id2", "2021-02-01 12:08:51 UTC", "PHOTO"),
                    new Memory("id3", "2021-01-29 18:22:18 UTC", "VIDEO")
            };

            new MemoriesHistoryFile(filePath).Memories().Should().BeEquivalentTo(expectedMemories);
        }
    }
}