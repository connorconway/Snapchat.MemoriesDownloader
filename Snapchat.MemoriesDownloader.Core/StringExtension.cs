namespace Snapchat.MemoriesDownloader.Core
{
    public static class StringExtension
    {
        public static string EnsureEndsWith(this string s, string ending) => s.EndsWith(ending) ? s : s + ending;
    }
}