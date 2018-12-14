using System;
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

        public IEnumerable<string> GetMarkdownFiles()
        {
            var fullRootPath = _fileSystem.Path.GetFullPath(_rootPath);
            var markdownFullPaths = _fileSystem.Directory.GetFiles(fullRootPath, "*.md", SearchOption.AllDirectories);
            var baseUri = new Uri(_fileSystem.Path.Combine(fullRootPath, "."));
            var relativePaths = markdownFullPaths.Select(fullPath => baseUri.MakeRelativeUri(new Uri(fullPath)).ToString());
            return relativePaths;
        }
    }
}
