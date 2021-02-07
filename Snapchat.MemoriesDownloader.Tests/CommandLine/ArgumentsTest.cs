using NUnit.Framework;
using Snapchat.MemoriesDownloader.CommandLine;

namespace Snapchat.MemoriesDownloader.Tests.CommandLine
{
    [TestFixture]
    public class ArgumentsTest
    {
        [Test]
        public void Parse()
        {
            Assert.AreEqual(@"memories_history.json", Arguments.Parse().MemoriesHistoryFilePath);
            Assert.AreEqual(@"memoriesfilepath", Arguments.Parse("--m", "memoriesfilepath").MemoriesHistoryFilePath);
            Assert.AreEqual(@"memoriesfilepath", Arguments.Parse("/m", "memoriesfilepath").MemoriesHistoryFilePath);
            Assert.AreEqual(@"memoriesfilepath", Arguments.Parse("--memoriesfile", "memoriesfilepath").MemoriesHistoryFilePath);

            Assert.AreEqual(@"Output", Arguments.Parse().OutputDirectoryPath);
            Assert.AreEqual(@"NewOutput\Folder", Arguments.Parse("--o", @"NewOutput\Folder").OutputDirectoryPath);
            Assert.AreEqual(@"NewOutput\Folder", Arguments.Parse("/o", @"NewOutput\Folder").OutputDirectoryPath);
            Assert.AreEqual(@"NewOutput\Folder", Arguments.Parse("--outputdirectory", @"NewOutput\Folder").OutputDirectoryPath);
        }
    }
}