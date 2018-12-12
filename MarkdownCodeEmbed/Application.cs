using MarkdownCodeEmbed.Factory;
using System;
using System.IO.Abstractions;
using Unity;

namespace MarkdownCodeEmbed
{
    internal class Application
    {
        private readonly IUnityContainer _container = new MainContainer();

        public void Run(InputArgs args)
        {
            Console.WriteLine("Working directory: " + _container.Resolve<IFileSystem>().Path.GetFullPath("."));

            var inputContainer = _container
                .Resolve<IInputContainerFactory>()
                .GetInputContainer(args.InputDirectory);

            var codeContainer = _container
                .Resolve<ICodeContainerFactory>()
                .GetCodeContainer(args.SourceCodeDirectory);

            var outputContainer = _container
                .Resolve<IOutputContainerFactory>()
                .GetOutputContainer(args.OutputDirectory);
        }
    }
}
