using MarkdownCodeEmbed.Factory;
using System.IO.Abstractions;
using Unity;

namespace MarkdownCodeEmbed
{
    internal class MainContainer : UnityContainer
    {
        public MainContainer()
        {
            Map<ICodeContainerFactory, CodeContainerFactory>();
            Map<IFileSystem, FileSystem>();
            Map<IInputContainerFactory, InputContainerFactory>();
            Map<IOutputContainerFactory, OutputContainerFactory>();
        }

        private void Map<TAbstraction, TImplementation>() where TImplementation : TAbstraction => this.RegisterType<TAbstraction, TImplementation>();
    }
}
