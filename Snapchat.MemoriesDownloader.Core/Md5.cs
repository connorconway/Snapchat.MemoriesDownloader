using System;
using System.IO;
using System.Security.Cryptography;

namespace Snapchat.MemoriesDownloader.Core
{
    public static class Md5
    {
        public static string CalculateFrom(string filePath)
        {
            using var sha256 = MD5.Create();
            using var stream = File.OpenRead(filePath);
            var hash = sha256.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public static string CalculateFrom(byte[] data)
        {
            using var sha256 = new MD5CryptoServiceProvider();
            var hash = sha256.ComputeHash(data);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}