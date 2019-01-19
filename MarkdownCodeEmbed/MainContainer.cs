using MarkdownCodeEmbed.Converter;
using MarkdownCodeEmbed.Factory;
using MarkdownCodeEmbed.Logger;
using System.IO.Abstractions;
using Unity;

namespace MarkdownCodeEmbed
{
    internal class MainContainer : UnityContainer
    {
        public MainContainer()
        {
            Map<ICodeContainerFactory, CodeContainerFactory>();
            Map<IFileToMarkdownConverter, FileToMarkdownConverter>();
            Map<IFileSystem, FileSystem>();
            Map<IInputContainerFactory, InputContainerFactory>();
            Map<ILogger, ConsoleLogger>();
            Map<IOutputContainerFactory, OutputContainerFactory>();
        }

        private void Map<TAbstraction, TImplementation>() where TImplementation : TAbstraction => this.RegisterType<TAbstraction, TImplementation>();
    }
}
