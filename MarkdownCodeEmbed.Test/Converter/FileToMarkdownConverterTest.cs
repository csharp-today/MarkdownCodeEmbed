using LucidCode;
using MarkdownCodeEmbed.Converter;
using Ninject;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace MarkdownCodeEmbed.Test.Converter
{
    public class FileToMarkdownConverterTest : TestContainer
    {
        private const string ExpectedCode = "SOME_TEST_CODE";

        [Fact]
        public void Return_CSharp_Markdown() => TestFileExtension("csharp", "cs");

        [Fact]
        public void Return_Unknown_Markdown() => TestFileExtension("", "xxx");

        [Fact]
        public void Return_Xml_Markdown() => TestFileExtension("xml", "xml");

        [Fact]
        public void Return_Xml_Markdown_For_CSProj_Files() => TestFileExtension("xml", "csproj");

        private void TestFileExtension(string expectedCodeBlockType, string fileExtension) => LucidTest
            .Arrange(() => ExpectedCode)
            .Act(code => Kernel.Get<FileToMarkdownConverter>().GetMarkdown("file." + fileExtension, code))
            .Assert(markdown =>
            {
                var lines = markdown.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                lines.Length.ShouldBe(3);

                const string CodeBlockMarker = "```";
                var firstLine = lines.First();
                firstLine.StartsWith(CodeBlockMarker).ShouldBeTrue();

                var blockType = firstLine.Replace(CodeBlockMarker, "");
                blockType.ShouldBe(expectedCodeBlockType);

                var secondLine = lines.Skip(1).First();
                secondLine.ShouldBe(ExpectedCode);

                var lastLine = lines.Last();
                lastLine.ShouldBe(CodeBlockMarker);
            });
    }
}
