using MarkdownCodeEmbed.Container;
using System;
using System.IO.Abstractions;

namespace MarkdownCodeEmbed.Factory
{
    internal class InputContainerFactory : IInputContainerFactory
    {
        private readonly IFileSystem _fileSystem;

        public InputContainerFactory(IFileSystem fileSystem) => _fileSystem = fileSystem;

        public InputContainer GetInputContainer(string inputDirectory)
        {
            if (!_fileSystem.Directory.Exists(inputDirectory))
            {
                throw new ArgumentException("Input directory doesn't exist: " + inputDirectory);
            }

            return new InputContainer(_fileSystem, inputDirectory);
        }
    }
}
