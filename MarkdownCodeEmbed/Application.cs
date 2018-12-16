using MarkdownCodeEmbed.Factory;
using System;
using System.IO.Abstractions;

namespace MarkdownCodeEmbed
{
    internal class Application
    {
        private readonly IFileSystem _fileSystem;
        private readonly IInputContainerFactory _inputContainerFactory;
        private readonly ICodeContainerFactory _codeContainerFactory;
        private readonly IOutputContainerFactory _outputContainerFactory;

        public Application(IFileSystem fileSystem, IInputContainerFactory inputContainerFactory, ICodeContainerFactory codeContainerFactory, IOutputContainerFactory outputContainerFactory)
        {
            _fileSystem = fileSystem;
            _inputContainerFactory = inputContainerFactory;
            _codeContainerFactory = codeContainerFactory;
            _outputContainerFactory = outputContainerFactory;
        }

        public void Run(InputArgs args)
        {
            Console.WriteLine("Working directory: " + _fileSystem.Path.GetFullPath("."));

            var inputContainer = _inputContainerFactory.GetInputContainer(args.InputDirectory);
            var codeContainer = _codeContainerFactory.GetCodeContainer(args.SourceCodeDirectory);
            var outputContainer = _outputContainerFactory.GetOutputContainer(args.OutputDirectory);

            foreach (var markdownFile in inputContainer.GetMarkdownFiles())
            {
                Console.WriteLine(">>> Content of: " + markdownFile);
                Console.WriteLine(markdownFile.Content);
            }
        }
    }
}
