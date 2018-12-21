using MarkdownCodeEmbed.Model;
using System.IO.Abstractions;

namespace MarkdownCodeEmbed.Container
{
    internal class OutputContainer
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _outputDirectory;

        public OutputContainer(IFileSystem fileSystem, string outputDirectory)
        {
            _fileSystem = fileSystem;
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

            _fileSystem.File.WriteAllText(fullPath, file.Content);
        }
    }
}
