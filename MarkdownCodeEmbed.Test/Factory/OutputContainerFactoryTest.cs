using LucidCode;
using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Factory;
using Ninject;
using Shouldly;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace MarkdownCodeEmbed.Test.Factory
{
    public class OutputContainerFactoryTest : TestContainer
    {
        public OutputContainerFactoryTest() => AddSelfBinding<OutputContainerFactory>();

        [Fact]
        public void Create_Output_Directory_When_Does_Not_Exist() => LucidTest
            .DefineExpected("directory-to-be-created")
            .Arrange(p => p)
            .Act(GetContainer)
            .Assert((outputDirectory, container) =>
            {
                container.ShouldNotBeNull();
                FileSystem.Directory.Exists(outputDirectory).ShouldBeTrue();
            });

        [Fact]
        public void Fail_When_Output_Directory_Is_Not_Empty() => LucidTest
            .Arrange(() =>
            {
                const string Directory = "not-empty-directory";
                FileSystem.AddFile(
                    FileSystem.Path.Combine(Directory, "file-name.txt"),
                    new MockFileData("content"));
                return Directory;
            })
            .Act(dir => GetException(() => GetContainer(dir)))
            .Assert(exception => exception.ShouldNotBeNull());

        [Fact]
        public void Get_Container_When_Have_Empty_Output_Directory() => LucidTest
            .Arrange(ArrangeExistingDirectory)
            .Act(GetContainer)
            .Assert(container => container.ShouldNotBeNull());

        private IOutputContainer GetContainer(string dir) => Kernel.Get<OutputContainerFactory>().GetOutputContainer(dir);
    }
}
