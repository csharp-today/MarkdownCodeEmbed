using MarkdownCodeEmbed.Container;

namespace MarkdownCodeEmbed.Factory
{
    internal interface ICodeContainerFactory
    {
        CodeContainer GetCodeContainer(string codeDirectory);
    }
}
