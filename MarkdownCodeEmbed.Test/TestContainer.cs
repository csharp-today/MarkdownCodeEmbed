using Ninject.MockingKernel.NSubstitute;
using System;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace MarkdownCodeEmbed.Test
{
    public class TestContainer : IDisposable
    {
        protected readonly MockFileSystem FileSystem = new MockFileSystem();
        protected readonly NSubstituteMockingKernel Kernel = new NSubstituteMockingKernel();

        public TestContainer() => Kernel.Bind<IFileSystem>().ToConstant(FileSystem);

        public void Dispose() => Kernel.Dispose();

        protected void AddSelfBinding<T>() => Kernel.Bind<T>().To<T>();

        protected string ArrangeExistingDirectory()
        {
            const string ExistingDirectory = "this-exist";
            FileSystem.AddDirectory(ExistingDirectory);
            return ExistingDirectory;
        }

        protected string CombinePath(params string[] paths) => FileSystem.Path.Combine(paths);

        protected Exception GetException(Action action)
        {
            try
            {
                action();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
