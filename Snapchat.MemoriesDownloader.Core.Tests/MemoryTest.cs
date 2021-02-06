using System.Net;
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
            const string url = "https://app.snapchat.com/dmd/memories?uid=e0471e2f-f875-4fa7-b4f2-d8b649d22b58&sid=2a2f9a03-5e78-2766-10f3-1fc88790f19f&mid=91ef3fac-f4bb-e0db-69ed-f02a26b3175e&ts=1612643680433&proxy=true&sig=c41cc3f017c3c7ed3de42812522b2cabef9de16a0557cc3269fec0c0930aaea7";
            var memory = new Memory(url, "", "VIDEO");
            var d = await memory.Download();
            d.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
        }
    }
}