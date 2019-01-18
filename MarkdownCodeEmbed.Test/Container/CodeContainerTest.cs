using LucidCode;
using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Converter;
using MarkdownCodeEmbed.Model;
using Ninject;
using NSubstitute;
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
                var markdown = $@"```csharp
{CodeFileContent}
```";
                return new
                {
                    CodeFileName,
                    CodeFileContent,
                    Markdown = markdown,
                    InputContent = string.Format(ContentFormatter, $"[embed-code]: # ({CodeFileName})"),
                    OutputContent = string.Format(ContentFormatter, markdown)
                };
            })
            .Arrange(param =>
            {
                const string CodePath = @"C:\code";
                string codeFilePath = CombinePath(CodePath, param.CodeFileName);
                FileSystem.AddFile(codeFilePath, new MockFileData(param.CodeFileContent));

                Kernel.Get<IFileToMarkdownConverter>().GetMarkdown(codeFilePath, param.CodeFileContent).Returns(param.Markdown);

                const string Name = "test.md";
                var markdownFile = new MarkdownFile(Name, CombinePath(@"C:\test", Name), Name)
                {
                    Content = param.InputContent
                };

                return new
                {
                    MarkdownFile = markdownFile,
                    CodePath
                };
            })
            .Act(param => new CodeContainer(Kernel.Get<IFileToMarkdownConverter>(), FileSystem, param.CodePath).EmbedCode(param.MarkdownFile))
            .Assert((expectedValues, file) => file.Content.ShouldBe(expectedValues.OutputContent));
    }
}
