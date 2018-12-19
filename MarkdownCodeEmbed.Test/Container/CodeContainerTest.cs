using LucidCode;
using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Model;
using Shouldly;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace MarkdownCodeEmbed.Test.Container
{
    public class CodeContainerTest : TestContainer
    {
        [Fact]
        public void Embed_Code_In_Markdown_File() => LucidTest
            .DefineExpected(() =>
            {
                const string ContentFormatter = @"# Header
{0}

More content";
                const string CodeFileName = "code.cs";
                const string CodeFileContent = "public void MethodName() { }";
                return new
                {
                    CodeFileName = CodeFileName,
                    CodeFileContent = CodeFileContent,
                    InputContent = string.Format(ContentFormatter, $"[embed-code]: # ({CodeFileName})"),
                    OutputContent = string.Format(ContentFormatter, $@"```csharp
{CodeFileContent}
```")
                };
            })
            .Arrange(param =>
            {
                const string CodePath = @"C:\code";
                FileSystem.AddFile(CombinePath(CodePath, param.CodeFileName), new MockFileData(param.CodeFileContent));

                const string Name = "test.md";
                var markdownFile = new MarkdownFile(Name, CombinePath(@"C:\test", Name), Name)
                {
                    Content = param.InputContent
                };

                return new
                {
                    MarkdownFile = markdownFile,
                    CodePath = CodePath
                };
            })
            .Act(param => new CodeContainer(FileSystem, param.CodePath).EmbedCode(param.MarkdownFile))
            .Assert((expectedValues, file) => file.Content.ShouldBe(expectedValues.OutputContent));
    }
}
