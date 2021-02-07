using System.Threading.Tasks;
using Snapchat.MemoriesDownloader.CommandLine;
using Snapchat.MemoriesDownloader.Core;

namespace Snapchat.MemoriesDownloader
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var commandLineArgs = Arguments.Parse(args);
            if (commandLineArgs == Arguments.Help) return;

            var memoriesHistoryFile = new MemoriesHistoryFile(commandLineArgs.MemoriesHistoryFilePath).Memories();
            foreach (var memory in memoriesHistoryFile)
            {
                await memory.SaveAsync();
            }
        }
    }
}