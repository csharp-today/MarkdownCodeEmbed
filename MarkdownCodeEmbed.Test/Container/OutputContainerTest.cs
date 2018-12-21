using LucidCode;
using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Model;
using Shouldly;
using Xunit;

namespace MarkdownCodeEmbed.Test.Container
{
    public class OutputContainerTest : TestContainer
    {
        [Fact]
        public void Save_File_In_Proper_Directory() => LucidTest
            .DefineExpected(() =>
            {
                const string FileName = "file.md";
                return new
                {
                    FileName,
                    OutputDirectory = @"C:\output",
                    RelativeOutputFilePath = @"test\" + FileName,
                    Content = "# Markdown file"
                };
            })
            .Arrange(values =>
            {
                FileSystem.AddDirectory(values.OutputDirectory);

                var file = new MarkdownFile(values.FileName, values.RelativeOutputFilePath, values.RelativeOutputFilePath);
                file.Content = values.Content;

                return new
                {
                    Container = new OutputContainer(FileSystem, values.OutputDirectory),
                    File = file
                };
            })
            .Act(param => param.Container.Save(param.File))
            .Assert(expectedValues =>
            {
                var filePath = CombinePath(expectedValues.OutputDirectory, expectedValues.RelativeOutputFilePath);
                FileSystem.File.Exists(filePath).ShouldBeTrue();
                FileSystem.File.ReadAllText(filePath).ShouldBe(expectedValues.Content);
            });
    }
}
