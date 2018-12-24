using MarkdownCodeEmbed.Container;

namespace MarkdownCodeEmbed.Factory
{
    internal interface IOutputContainerFactory
    {
        IOutputContainer GetOutputContainer(string outputDirectory);
    }
}
