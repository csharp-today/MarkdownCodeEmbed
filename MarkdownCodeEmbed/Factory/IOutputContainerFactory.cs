using MarkdownCodeEmbed.Container;

namespace MarkdownCodeEmbed.Factory
{
    internal interface IOutputContainerFactory
    {
        OutputContainer GetOutputContainer(string outputDirectory);
    }
}
