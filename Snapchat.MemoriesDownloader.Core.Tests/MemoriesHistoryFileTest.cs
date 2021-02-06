using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Snapchat.MemoriesDownloader.Core.Tests
{
    [TestFixture]
    public class MemoriesHistoryFileTest
    {
        [Test]
        public void MemoriesHistoryDto()
        {
            var filePath = Path.Combine("TestData", MemoriesHistoryFile.Name);
            var expectedResult = new MemoriesHistoryDto
            {
                    SavedMedia = new[]
                    {
                            new SavedMediaDto
                            {
                                    Date = "2021-02-03 20:47:45 UTC",
                                    MediaType = "VIDEO",
                                    DownloadLink = "download link 1"
                            },
                            new SavedMediaDto
                            {
                                    Date = "2021-02-01 12:08:51 UTC",
                                    MediaType = "PHOTO",
                                    DownloadLink = "download link 2"
                            },
                            new SavedMediaDto
                            {
                                    Date = "2021-01-29 18:22:18 UTC",
                                    MediaType = "VIDEO",
                                    DownloadLink = "download link 3"
                            }
                    }
            };

            new MemoriesHistoryFile(filePath).Memories.Should().BeEquivalentTo(expectedResult);
        }
    }
}