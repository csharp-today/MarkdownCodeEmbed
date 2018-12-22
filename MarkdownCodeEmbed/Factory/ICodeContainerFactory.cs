using MarkdownCodeEmbed.Container;

namespace MarkdownCodeEmbed.Factory
{
    internal interface ICodeContainerFactory
    {
        ICodeContainer GetCodeContainer(string codeDirectory);
    }
}
