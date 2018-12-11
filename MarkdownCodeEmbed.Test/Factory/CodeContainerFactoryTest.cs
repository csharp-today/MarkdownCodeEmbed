using LucidCode;
using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Factory;
using Xunit;
using Ninject;
using Shouldly;

namespace MarkdownCodeEmbed.Test.Factory
{
    public class CodeContainerFactoryTest : TestContainer
    {
        public CodeContainerFactoryTest() => AddSelfBinding<CodeContainerFactory>();

        [Fact]
        public void Fail_When_Code_Directory_Do_Not_Exist() => LucidTest
            .Arrange(() => "not-existing-directory")
            .Act(dir => GetException(() => GetContainer(dir)))
            .Assert(exception => exception.ShouldNotBeNull());

        [Fact]
        public void Get_Container_When_Code_Directory_Exists() => LucidTest
            .Arrange(ArrangeExistingDirectory)
            .Act(GetContainer)
            .Assert(container => container.ShouldNotBeNull());

        private CodeContainer GetContainer(string dir) => Kernel.Get<CodeContainerFactory>().GetCodeContainer(dir);
    }
}
