using MarkdownCodeEmbed.Factory;
using System.IO.Abstractions;
using Unity;

namespace MarkdownCodeEmbed
{
    internal class MainContainer : UnityContainer
    {
        public MainContainer()
        {
            this.RegisterType<IFileSystem, FileSystem>();
            this.RegisterType<IInputContainerFactory, InputContainerFactory>();
        }
    }
}
