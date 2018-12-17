using LucidCode;
using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Model;
using Shouldly;
using Xunit;

namespace MarkdownCodeEmbed.Test.Container
{
    public class CodeContainerTest : TestContainer
    {
        [Fact]
        public void Replace_Code_Embed_Marks() => LucidTest
            .DefineExpected(@"```csharp
public void TestCodeEmbed() { }
```")
            .Arrange(_ =>
            {
                const string Name = "test.md";
                var markdownFile = new MarkdownFile(Name, CombinePath(@"C:\test", Name), Name)
                {
                    Content = "[embed-code]: # (code.cs)"
                };
                return markdownFile;
            })
            .Act(file => new CodeContainer().EmbedCode(file))
            .Assert((expectedCode, file) => file.Content.ShouldBe(expectedCode));
    }
}
