using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var memories = new MemoriesHistoryFile(commandLineArgs.MemoriesHistoryFilePath).Memories();
            var currentHashes = Directory.GetFiles(".", "*.*", SearchOption.AllDirectories).Select(Md5.CalculateFrom).ToList();

            foreach (var memory in memories)
            {
                if (!memory.Exists())
                {
                    var memoryHash = await memory.HashAsync();
                    if (!currentHashes.Contains(memoryHash))
                    {
                        currentHashes.Add(memoryHash);
                        await memory.SaveAsync();
                        memory.Dispose();
                    }
                    else
                    {
                        Console.WriteLine("Skipping - memory already exists.");
                    }
                }
            }
        }
    }
}