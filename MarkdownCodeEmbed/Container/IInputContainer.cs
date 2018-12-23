using MarkdownCodeEmbed.Model;
using System.Collections.Generic;

namespace MarkdownCodeEmbed.Container
{
    interface IInputContainer
    {
        IEnumerable<MarkdownFile> GetMarkdownFiles();
    }
}
