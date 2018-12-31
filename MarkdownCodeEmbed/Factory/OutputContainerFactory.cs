using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Logger;
using System.IO.Abstractions;
using System.Linq;

namespace MarkdownCodeEmbed.Factory
{
    internal class OutputContainerFactory : IOutputContainerFactory
    {
        private readonly IFileSystem _fileSystem;
        private readonly ILogger _logger;

        public OutputContainerFactory(IFileSystem fileSystem, ILogger logger)
        {
            _fileSystem = fileSystem;
            _logger = logger;
        }

        public IOutputContainer GetOutputContainer(string outputDirectory)
        {
            if (_fileSystem.Directory.Exists(outputDirectory))
            {
                if (_fileSystem.Directory.GetFiles(outputDirectory).Any())
                {
                    _logger.Log("Warning: The output directory is not empty, some files might be overridden.");
                }
            }
            else
            {
                _fileSystem.Directory.CreateDirectory(outputDirectory);
            }

            return new OutputContainer(_fileSystem, _logger, outputDirectory);
        }
    }
}
