using MarkdownCodeEmbed.Factory;
using MarkdownCodeEmbed.Logger;
using System.IO.Abstractions;

namespace MarkdownCodeEmbed
{
    internal class Application
    {
        private readonly IFileSystem _fileSystem;
        private readonly IInputContainerFactory _inputContainerFactory;
        private readonly ICodeContainerFactory _codeContainerFactory;
        private readonly ILogger _logger;
        private readonly IOutputContainerFactory _outputContainerFactory;

        public Application(IFileSystem fileSystem, IInputContainerFactory inputContainerFactory, ICodeContainerFactory codeContainerFactory, ILogger logger, IOutputContainerFactory outputContainerFactory)
        {
            _fileSystem = fileSystem;
            _inputContainerFactory = inputContainerFactory;
            _codeContainerFactory = codeContainerFactory;
            _logger = logger;
            _outputContainerFactory = outputContainerFactory;
        }

        public void Run(InputArgs args)
        {
            _logger.Log("Working directory: " + _fileSystem.Path.GetFullPath("."));

            var inputContainer = _inputContainerFactory.GetInputContainer(args.InputDirectory);
            //var codeContainer = _codeContainerFactory.GetCodeContainer(args.SourceCodeDirectory);
            //var outputContainer = _outputContainerFactory.GetOutputContainer(args.OutputDirectory);

            foreach (var markdownFile in inputContainer.GetMarkdownFiles())
            {
                _logger.Log("Processing: " + markdownFile.RelativePath);
                //var updatedFile = codeContainer.EmbedCode(markdownFile);
                //_logger.Log("Updated content: " + updatedFile.Content);
            }
        }
    }
}
