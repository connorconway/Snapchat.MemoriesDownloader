using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Snapchat.MemoriesDownloader.Core.FileSystem
{
    public class MemoryDirectory : CompositeMemoryFile
    {
        public MemoryDirectory(string fullPath) : base(fullPath.EnsureEndsWith("\\")) { }

        public override void Accept(FileVisitor visitor)
        {
            visitor.PreVisit(this);
            Files.ForEach(f => f.Accept(visitor));
            visitor.PostVisit(this);
        }

        protected override CompositeMemoryFile EmptyComposite() => new MemoryDirectory(Path);
    }

    public class MemoryFile : IFile
    {
        public MemoryFile(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath)) throw new ArgumentException(nameof(fullPath));
            FullPath = fullPath;
        }

        public void Accept(FileVisitor visitor)
        {
            visitor.PreVisit(this);
            visitor.PostVisit(this);
        }

        public string PrettyPrint() => ToString();

        public string FullPath { get; }

        public string Name => System.IO.Path.GetFileName(FullPath);

        public override string ToString() => $"{FullPath}";
    }

    public abstract class CompositeMemoryFile : IFile, IEnumerable<IFile>
    {
        protected readonly List<IFile> Files = new List<IFile>();
        protected readonly string Path;

        protected CompositeMemoryFile(string path) => Path = path;

        public void Add(IFile file) => Files.Add(file);

        public void Delete(IFile file)
        {
            if (Files.Remove(file))
                return;

            var compositeFiles = Files.OfType<CompositeMemoryFile>();
            foreach (var compositeFile in compositeFiles)
                compositeFile.Delete(file);
        }

        public string PrettyPrint()
        {
            var visitor = new PrettyPrintVisitor();
            Accept(visitor);
            return visitor.Result();
        }

        public abstract void Accept(FileVisitor visitor);

        public IEnumerator<IFile> GetEnumerator() => Files.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => Path;

        protected abstract CompositeMemoryFile EmptyComposite();

        public string Name => System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(Path)) ?? string.Empty;
        public string FullPath => Path;
    }

    public interface IFile
    {
        void Accept(FileVisitor visitor);
        string PrettyPrint();
    }

    public abstract class FileVisitor
    {
        public virtual void PreVisit(MemoryFile f) { }

        public virtual void PostVisit(MemoryFile f) { }

        public virtual void PreVisit(MemoryDirectory d) { }

        public virtual void PostVisit(MemoryDirectory d) { }
    }
}