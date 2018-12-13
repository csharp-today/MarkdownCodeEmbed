using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace MarkdownCodeEmbed.Container
{
    internal class InputContainer
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _rootPath;

        public InputContainer(IFileSystem fileSystem, string rootPath)
        {
            _fileSystem = fileSystem;
            _rootPath = rootPath;
        }

        public IEnumerable<string> GetMarkdownFiles() => _fileSystem.Directory
            .GetFiles(_rootPath, "*.md", SearchOption.AllDirectories);
    }
}
