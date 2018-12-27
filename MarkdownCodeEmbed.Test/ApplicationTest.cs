using LucidCode;
using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Factory;
using MarkdownCodeEmbed.Model;
using Ninject;
using NSubstitute;
using Xunit;

namespace MarkdownCodeEmbed.Test
{
    public class ApplicationTest : TestContainer
    {
        private const string CodeDirectory = @"C:\code\dir";
        private const string InputDirectory = @"C:\input\dir";
        private const string OutputDirectory = @"C:\output\dir";

        private readonly ICodeContainer _codeContainer;
        private readonly IInputContainer _inputContainer;
        private readonly IOutputContainer _outputContainer;

        private readonly InputArgs _args = new InputArgs
        {
            InputDirectory = InputDirectory,
            SourceCodeDirectory = CodeDirectory,
            OutputDirectory = OutputDirectory
        };

        public ApplicationTest()
        {
            _codeContainer = Kernel.Get<ICodeContainer>();
            Kernel.Get<ICodeContainerFactory>().GetCodeContainer(CodeDirectory).Returns(_codeContainer);

            _inputContainer = Kernel.Get<IInputContainer>();
            Kernel.Get<IInputContainerFactory>().GetInputContainer(InputDirectory).Returns(_inputContainer);

            _outputContainer = Kernel.Get<IOutputContainer>();
            Kernel.Get<IOutputContainerFactory>().GetOutputContainer(OutputDirectory).Returns(_outputContainer);
        }

        [Fact]
        public void Embed_Code_In_Every_Input_File() => LucidTest
            .DefineExpected(new[] { CreateFile(), CreateFile() })
            .Arrange(inputFiles =>
            {
                _inputContainer.GetMarkdownFiles().Returns(inputFiles);
                _codeContainer.EmbedCode(null).ReturnsForAnyArgs(CreateFile());
            })
            .Act(RunApplication)
            .Assert(inputFiles =>
            {
                foreach (var inputFile in inputFiles)
                {
                    Kernel.Get<ICodeContainer>().Received().EmbedCode(inputFile);
                }
            });

        [Fact]
        public void Get_Markdown_Input_Files() => LucidTest
            .Arrange(() => { _inputContainer.GetMarkdownFiles().Returns(new MarkdownFile[0]); })
            .Act(RunApplication)
            .Assert(() => Kernel.Get<IInputContainer>().Received().GetMarkdownFiles());

        [Fact]
        public void Save_Files_With_Embeded_Code() => LucidTest
            .DefineExpected(new
            {
                InputFiles = new[] { CreateFile(), CreateFile() },
                FilesWithEmbededCode = new[] { CreateFile(), CreateFile() }
            })
            .Arrange(values =>
            {
                _inputContainer.GetMarkdownFiles().Returns(values.InputFiles);
                _codeContainer.EmbedCode(values.InputFiles[0]).Returns(values.FilesWithEmbededCode[0]);
                _codeContainer.EmbedCode(values.InputFiles[1]).Returns(values.FilesWithEmbededCode[1]);
            })
            .Act(RunApplication)
            .Assert(files =>
            {
                _outputContainer.Received().Save(files.FilesWithEmbededCode[0]);
                _outputContainer.Received().Save(files.FilesWithEmbededCode[1]);
                _outputContainer.DidNotReceive().Save(files.InputFiles[0]);
                _outputContainer.DidNotReceive().Save(files.InputFiles[1]);
            });

        private MarkdownFile CreateFile() => new MarkdownFile("", "", "");

        private void RunApplication() => Kernel.Get<Application>().Run(_args);
    }
}
