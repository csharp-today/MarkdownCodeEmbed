using LucidCode;
using MarkdownCodeEmbed.Converter;
using Ninject;
using Shouldly;
using Xunit;

namespace MarkdownCodeEmbed.Test.Converter
{
    public class FileToMarkdownConverterTest : TestContainer
    {
        [Theory]
        [InlineData("file.cs"), InlineData("other.xxx")]
        public void Always_Return_CSharp_Markdown(string fileName) => LucidTest
            .DefineExpected(@"```csharp
TEST
```")
            .Arrange(_ => "TEST")
            .Act(content => Kernel.Get<FileToMarkdownConverter>().GetMarkdown(fileName, content))
            .Assert((expected, result) => result.ShouldBe(expected));
    }
}
