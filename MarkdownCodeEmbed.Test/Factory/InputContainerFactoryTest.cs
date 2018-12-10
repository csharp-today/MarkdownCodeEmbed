using LucidCode;
using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Factory;
using Ninject;
using Ninject.MockingKernel;
using Shouldly;
using System;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace MarkdownCodeEmbed.Test.Factory
{
    public class InputContainerFactoryTest : IDisposable
    {
        private readonly MockFileSystem _fileSystem = new MockFileSystem();
        private readonly MockingKernel _kernel = new MockingKernel();

        public InputContainerFactoryTest()
        {
            _kernel.Bind<IFileSystem>().ToConstant(_fileSystem);
            _kernel.Bind<InputContainerFactory>().To<InputContainerFactory>();
        }

        public void Dispose() => _kernel.Dispose();

        [Fact]
        public void Fail_When_Input_Directory_Do_Not_Exist() => LucidTest
            .Arrange(() => "not-existing-directory-name")
            .Act(dir =>
            {
                try
                {
                    GetContainer(dir);
                    return null;
                }
                catch (Exception ex)
                {
                    return ex;
                }
            })
            .Assert(exception => exception.ShouldNotBeNull());

        [Fact]
        public void Get_Container_When_Input_Directory_Exists() => LucidTest
            .Arrange(() =>
            {
                const string ExistingDirectory = "this-exist";
                _fileSystem.AddDirectory(ExistingDirectory);
                return ExistingDirectory;
            })
            .Act(GetContainer)
            .Assert(container => container.ShouldNotBeNull());

        private InputContainer GetContainer(string dir) => _kernel.Get<InputContainerFactory>().GetInputContainer(dir);
    }
}
