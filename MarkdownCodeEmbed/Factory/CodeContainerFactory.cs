using MarkdownCodeEmbed.Container;
using System;
using System.IO.Abstractions;

namespace MarkdownCodeEmbed.Factory
{
    internal class CodeContainerFactory : ICodeContainerFactory
    {
        private readonly IFileSystem _fileSystem;

        public CodeContainerFactory(IFileSystem fileSystem) => _fileSystem = fileSystem;

        public ICodeContainer GetCodeContainer(string codeDirectory)
        {
            if (!_fileSystem.Directory.Exists(codeDirectory))
            {
                throw new ArgumentException("Code directory doesn't exist: " + codeDirectory);
            }

            return new CodeContainer(_fileSystem, codeDirectory);
        }
    }
}
