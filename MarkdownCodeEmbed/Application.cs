using MarkdownCodeEmbed.Factory;
using Unity;

namespace MarkdownCodeEmbed
{
    internal class Application
    {
        private readonly IUnityContainer _container = new MainContainer();

        public void Run(InputArgs args)
        {
            var inputContainer = _container
                .Resolve<IInputContainerFactory>()
                .GetInputContainer(args.InputDirectory);

            var codeContainer = _container
                .Resolve<ICodeContainerFactory>()
                .GetCodeContainer(args.SourceCodeDirectory);
        }
    }
}
