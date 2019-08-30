using MarkdownCodeEmbed.Logger;
using MarkdownCodeEmbed.Model;
using System.IO.Abstractions;

namespace MarkdownCodeEmbed.Container
{
    internal class OutputContainer : IOutputContainer
    {
        private readonly IFileSystem _fileSystem;
        private readonly ILogger _logger;
        private readonly string _outputDirectory;

        public OutputContainer(IFileSystem fileSystem, ILogger logger, string outputDirectory)
        {
            _fileSystem = fileSystem;
            _logger = logger;
            _outputDirectory = outputDirectory;
        }

        public void Save(MarkdownFile file)
        {
            var fullPath = _fileSystem.Path.Combine(_outputDirectory, file.RelativePath);

            var parentDirectory = _fileSystem.Path.GetDirectoryName(fullPath);
            if (!_fileSystem.Directory.Exists(parentDirectory))
            {
                _fileSystem.Directory.CreateDirectory(parentDirectory);
            }

            if (_fileSystem.File.Exists(fullPath))
            {
                _logger.Log("Warning: Overriding file " + file.RelativePath);
            }
            _fileSystem.File.WriteAllText(fullPath, file.Content);
        }
    }
}
