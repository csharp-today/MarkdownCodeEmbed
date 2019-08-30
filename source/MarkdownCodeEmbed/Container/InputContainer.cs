using MarkdownCodeEmbed.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace MarkdownCodeEmbed.Container
{
    internal class InputContainer : IInputContainer
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _rootPath;

        public InputContainer(IFileSystem fileSystem, string rootPath)
        {
            _fileSystem = fileSystem;
            _rootPath = rootPath;
        }

        public IEnumerable<MarkdownFile> GetMarkdownFiles()
        {
            var fullRootPath = _fileSystem.Path.GetFullPath(_rootPath);
            var baseUri = new Uri(_fileSystem.Path.Combine(fullRootPath, "."));

            var markdownFullPaths = _fileSystem.Directory.GetFiles(fullRootPath, "*.md", SearchOption.AllDirectories);
            foreach (var fullPath in markdownFullPaths)
            {
                var fileName = _fileSystem.Path.GetFileName(fullPath);
                var relativePath = baseUri.MakeRelativeUri(new Uri(fullPath)).ToString();

                // Return paths with consistent directory separators
                if (_fileSystem.Path.DirectorySeparatorChar != _fileSystem.Path.AltDirectorySeparatorChar)
                {
                    //                                  Windows Linux
                    // Path.DirectorySeparatorChar      \       /
                    // Path.AltDirectorySeparatorChar   /       /
                    //
                    // Path.GetFullPath always uses Path.DirectorySeparatorChar
                    // Uri.MakeRelativeUri always uses '/'
                    relativePath = relativePath.Replace(_fileSystem.Path.AltDirectorySeparatorChar, _fileSystem.Path.DirectorySeparatorChar);
                }

                yield return new MarkdownFile(fileName, fullPath, relativePath)
                {
                    Content = _fileSystem.File.ReadAllText(fullPath)
                };
            }
        }
    }
}
