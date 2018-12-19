using MarkdownCodeEmbed.Model;
using System;
using System.IO.Abstractions;
using System.Text.RegularExpressions;

namespace MarkdownCodeEmbed.Container
{
    internal class CodeContainer
    {
        private static readonly Regex EmbedCodeDirectiveRegex = new Regex(@"\[ *embed-code *\] *: *# *\((.*)\)", RegexOptions.IgnoreCase);

        private readonly IFileSystem _fileSystem;
        private readonly string _codePath;

        public CodeContainer(IFileSystem fileSystem, string codePath)
        {
            _fileSystem = fileSystem;
            _codePath = codePath;
        }

        public MarkdownFile EmbedCode(MarkdownFile inputFile)
        {
            var content = EmbedCodeDirectiveRegex.Replace(inputFile.Content, GetCode);
            return new MarkdownFile(inputFile.FileName, inputFile.RelativePath, inputFile.RelativePath)
            {
                Content = content
            };
        }

        private string GetCode(Match embedCodeDirective)
        {
            var path = GetCodeFilePath(embedCodeDirective);
            var content = _fileSystem.File.ReadAllText(path);

            return $@"```csharp
{content}
```";
        }

        private string GetCodeFilePath(Match embedCodeDirective)
        {
            var inputPath = embedCodeDirective.Groups[1].ToString();
            if (TryGetRelativeCodeFilePath(inputPath, out var fullPath))
            {
                return fullPath;
            }

            if (TryGetAbsoluteCodeFilePath(inputPath, out fullPath))
            {
                return fullPath;
            }

            throw new Exception("Can't find code file path: " + inputPath);
        }

        private bool TryGetAbsoluteCodeFilePath(string absolutePath, out string path)
        {
            if (_fileSystem.File.Exists(absolutePath))
            {
                path = absolutePath;
                return true;
            }

            path = null;
            return false;
        }

        private bool TryGetRelativeCodeFilePath(string relativePath, out string path)
        {
            var fullPath = _fileSystem.Path.Combine(_codePath, relativePath);
            return TryGetAbsoluteCodeFilePath(fullPath, out path);
        }
    }
}
