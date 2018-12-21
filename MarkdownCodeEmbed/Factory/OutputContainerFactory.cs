using MarkdownCodeEmbed.Container;
using System;
using System.IO.Abstractions;
using System.Linq;

namespace MarkdownCodeEmbed.Factory
{
    internal class OutputContainerFactory : IOutputContainerFactory
    {
        private readonly IFileSystem _fileSystem;

        public OutputContainerFactory(IFileSystem fileSystem) => _fileSystem = fileSystem;

        public OutputContainer GetOutputContainer(string outputDirectory)
        {
            if (_fileSystem.Directory.Exists(outputDirectory))
            {
                if (_fileSystem.Directory.GetFiles(outputDirectory).Any())
                {
                    throw new ArgumentException("Output directory is not empty: " + outputDirectory);
                }
            }
            else
            {
                _fileSystem.Directory.CreateDirectory(outputDirectory);
            }

            return new OutputContainer(_fileSystem, outputDirectory);
        }
    }
}
