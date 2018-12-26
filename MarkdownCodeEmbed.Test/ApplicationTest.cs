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
        private const string InputDirectory = @"C:\input\dir";

        private readonly IInputContainer _inputContainer;

        private InputArgs _args = new InputArgs { InputDirectory = InputDirectory };

        public ApplicationTest()
        {
            _inputContainer = Kernel.Get<IInputContainer>();
            Kernel.Get<IInputContainerFactory>().GetInputContainer(InputDirectory).Returns(_inputContainer);
        }

        [Fact]
        public void Embed_Code_In_Every_Input_File() => LucidTest
            .DefineExpected(new[] { new MarkdownFile("", "", ""), new MarkdownFile("", "", "") })
            .Arrange(inputFiles =>
            {
                _inputContainer.GetMarkdownFiles().Returns(inputFiles);

                var codeContainer = Kernel.Get<ICodeContainer>();
                codeContainer.EmbedCode(null).ReturnsForAnyArgs(new MarkdownFile("", "", ""));

                const string CodeDirectory = @"C:\code\dir";
                Kernel.Get<ICodeContainerFactory>().GetCodeContainer(CodeDirectory).Returns(codeContainer);

                _args = new InputArgs
                {
                    InputDirectory = InputDirectory,
                    SourceCodeDirectory = CodeDirectory
                };
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

        private void RunApplication() => Kernel.Get<Application>().Run(_args);
    }
}
