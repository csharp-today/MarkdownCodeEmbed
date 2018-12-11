using LucidCode;
using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Factory;
using Ninject;
using Shouldly;
using System;
using Xunit;

namespace MarkdownCodeEmbed.Test.Factory
{
    public class InputContainerFactoryTest : TestContainer
    {
        public InputContainerFactoryTest() => AddSelfBinding<InputContainerFactory>();

        [Fact]
        public void Fail_When_Input_Directory_Do_Not_Exist() => LucidTest
            .Arrange(() => "not-existing-directory-name")
            .Act(dir => GetException(() => GetContainer(dir)))
            .Assert(exception => exception.ShouldNotBeNull());

        [Fact]
        public void Get_Container_When_Input_Directory_Exists() => LucidTest
            .Arrange(ArrangeExistingDirectory)
            .Act(GetContainer)
            .Assert(container => container.ShouldNotBeNull());

        private InputContainer GetContainer(string dir) => Kernel.Get<InputContainerFactory>().GetInputContainer(dir);
    }
}
