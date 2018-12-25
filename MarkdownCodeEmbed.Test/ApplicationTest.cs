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
        [Fact]
        public void Get_Markdown_Input_Files() => LucidTest
            .Arrange(() =>
            {
                var inputContainer = Kernel.Get<IInputContainer>();
                inputContainer.GetMarkdownFiles().Returns(new MarkdownFile[0]);

                const string InputDirectory = @"C:\input\dir";
                Kernel.Get<IInputContainerFactory>().GetInputContainer(InputDirectory).Returns(inputContainer);

                return new InputArgs { InputDirectory = InputDirectory };
            })
            .Act(RunApplication)
            .Assert(() => Kernel.Get<IInputContainer>().Received().GetMarkdownFiles());

        private void RunApplication(InputArgs args) => Kernel.Get<Application>().Run(args);
    }
}
