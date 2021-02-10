using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Snapchat.MemoriesDownloader.Core.Tests
{
    [TestFixture]
    public class Sha256Test
    {
        private string _testImageFilePath;
        private string _testVideoFilePath;

        [SetUp]
        public void SetUp()
        {
            _testImageFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "TestImage.jpg");
            _testVideoFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "TestVideo.mp4");
        }

        [Test]
        public void CalculateFrom()
        {
            var fileHash = Md5.CalculateFrom(_testImageFilePath);
            var byteStringFileHash = Md5.CalculateFrom(File.ReadAllBytes(_testImageFilePath));
            fileHash.Should().BeEquivalentTo(byteStringFileHash).And.BeEquivalentTo("493eb052f503192f40688efe4c68eb220e898d6207e4edde10c89691b2dfa901");

            var videoHash = Md5.CalculateFrom(_testVideoFilePath);
            var byteStringVideoHash = Md5.CalculateFrom(File.ReadAllBytes(_testVideoFilePath));
            videoHash.Should().BeEquivalentTo(byteStringVideoHash).And.BeEquivalentTo("465c4e5e1e896aca42169e2d68af64bb8cf1bbb203b08d3a63bd6b9bc54fe91e");
        }
    }
}