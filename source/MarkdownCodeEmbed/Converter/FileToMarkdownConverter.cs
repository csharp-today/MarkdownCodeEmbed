﻿using System.IO.Abstractions;

namespace MarkdownCodeEmbed.Converter
{
    internal class FileToMarkdownConverter : IFileToMarkdownConverter
    {
        private readonly IFileSystem _fileSystem;

        public FileToMarkdownConverter(IFileSystem fileSystem) => _fileSystem = fileSystem;

        public string GetMarkdown(string filePath, string fileContent)
        {
            var type = GetCodeBlockType(filePath);
            return $@"```{type}
{fileContent}
```";
        }

        private string GetCodeBlockType(string filePath)
        {
            switch(_fileSystem.Path.GetExtension(filePath).ToLower())
            {
                case ".cs":
                    return "csharp";
                case ".css":
                    return "css";
                case ".csproj":
                case ".xml":
                    return "xml";
                case ".htm":
                case ".html":
                    return "html";
                case ".js":
                    return "javascript";
                default:
                    return null;
            }
        }
    }
}
