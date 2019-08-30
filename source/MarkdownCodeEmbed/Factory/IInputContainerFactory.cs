using MarkdownCodeEmbed.Container;

namespace MarkdownCodeEmbed.Factory
{
    internal interface IInputContainerFactory
    {
        IInputContainer GetInputContainer(string inputDirectory);
    }
}
