﻿using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Converter;
using System;
using System.IO.Abstractions;

namespace MarkdownCodeEmbed.Factory
{
    internal class CodeContainerFactory : ICodeContainerFactory
    {
        private readonly IFileSystem _fileSystem;
        private readonly IFileToMarkdownConverter _fileToMarkdownConverter;

        public CodeContainerFactory(IFileSystem fileSystem, IFileToMarkdownConverter fileToMarkdownConverter)
        {
            _fileSystem = fileSystem;
            _fileToMarkdownConverter = fileToMarkdownConverter;
        }

        public ICodeContainer GetCodeContainer(string codeDirectory)
        {
            if (!_fileSystem.Directory.Exists(codeDirectory))
            {
                throw new ArgumentException("Code directory doesn't exist: " + codeDirectory);
            }

            return new CodeContainer(_fileToMarkdownConverter, _fileSystem, codeDirectory);
        }
    }
}
