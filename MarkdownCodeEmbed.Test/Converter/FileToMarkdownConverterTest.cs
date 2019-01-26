using LucidCode;
using LucidCode.LucidTestFundations;
using MarkdownCodeEmbed.Converter;
using Ninject;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace MarkdownCodeEmbed.Test.Converter
{
    public static class FileToMarkdownConverterTestExtensions
    {
        public const string ExpectedCode = "SOME_TEST_CODE";

        public static void AssertCodeBlockIsRecognizedAs(this AssertManager<string> manager, string expectedBlockType)
        {
            var lines = manager.ActResult.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            lines.Length.ShouldBe(3);

            const string CodeBlockMarker = "```";
            var firstLine = lines.First();
            firstLine.StartsWith(CodeBlockMarker).ShouldBeTrue();

            var blockType = firstLine.Replace(CodeBlockMarker, "");
            blockType.ShouldBe(expectedBlockType);

            var secondLine = lines.Skip(1).First();
            secondLine.ShouldBe(ExpectedCode);

            var lastLine = lines.Last();
            lastLine.ShouldBe(CodeBlockMarker);
        }
    }

    public class FileToMarkdownConverterTest : TestContainer
    {
        [Fact]
        public void Return_CSharp_Markdown() => LucidTest
            .Arrange(GetDefaultCodeBlock)
            .Act(code => GetMarkdown("file.cs", code))
            .AssertCodeBlockIsRecognizedAs("csharp");

        [Fact]
        public void Return_Unknown_Markdown() => LucidTest
            .Arrange(GetDefaultCodeBlock)
            .Act(code => GetMarkdown("other.xxx", code))
            .AssertCodeBlockIsRecognizedAs("");

        [Fact]
        public void Return_Xml_Markdown() => LucidTest
            .Arrange(GetDefaultCodeBlock)
            .Act(code => GetMarkdown("doc.xml", code))
            .AssertCodeBlockIsRecognizedAs("xml");

        private string GetDefaultCodeBlock() => FileToMarkdownConverterTestExtensions.ExpectedCode;

        private string GetMarkdown(string fileName, string content) => Kernel.Get<FileToMarkdownConverter>().GetMarkdown(fileName, content);
    }
}
