using System;
using System.IO;
using System.Security.Cryptography;

namespace Snapchat.MemoriesDownloader.Core
{
    public static class Sha256
    {
        public static string CalculateFrom(string filePath)
        {
            using var sha256 = SHA256.Create();
            using var stream = File.OpenRead(filePath);
            var hash = sha256.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public static string CalculateFrom(byte[] data)
        {
            using var sha256 = new SHA256CryptoServiceProvider();
            var hash = sha256.ComputeHash(data);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}