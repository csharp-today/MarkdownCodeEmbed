using MarkdownCodeEmbed.Model;

namespace MarkdownCodeEmbed.Container
{
    interface IOutputContainer
    {
        void Save(MarkdownFile file);
    }
}
