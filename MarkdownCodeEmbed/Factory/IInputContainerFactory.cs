using MarkdownCodeEmbed.Container;

namespace MarkdownCodeEmbed.Factory
{
    internal interface IInputContainerFactory
    {
        InputContainer GetInputContainer(string inputDirectory);
    }
}
