using System.Text;

namespace Snapchat.MemoriesDownloader.Core.FileSystem
{
    public class PrettyPrintVisitor : FileVisitor
    {
        private readonly StringBuilder _result = new StringBuilder();
        private int _tabCount;

        private void Render(IFile f)
        {
            _result.Append(new string('.', _tabCount * 2));
            _result.Append(f);
            _result.Append("\n");
            _tabCount++;
        }

        public override void PreVisit(MemoryFile f) => Render(f);

        public override void PostVisit(MemoryFile f) => _tabCount--;

        public override void PreVisit(MemoryDirectory d) => Render(d);

        public override void PostVisit(MemoryDirectory d) => _tabCount--;

        public string Result() => _result.ToString();
    }
}