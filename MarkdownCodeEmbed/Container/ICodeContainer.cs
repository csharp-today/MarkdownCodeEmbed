using MarkdownCodeEmbed.Model;

namespace MarkdownCodeEmbed.Container
{
    internal interface ICodeContainer
    {
        MarkdownFile EmbedCode(MarkdownFile inputFile);
    }
}
