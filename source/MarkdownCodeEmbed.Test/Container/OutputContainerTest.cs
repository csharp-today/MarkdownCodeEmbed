using LucidCode;
using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Logger;
using MarkdownCodeEmbed.Model;
using Ninject;
using NSubstitute;
using Shouldly;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace MarkdownCodeEmbed.Test.Container
{
    public class OutputContainerTest : TestContainer
    {
        private ILogger Logger => Kernel.Get<ILogger>();

        [Fact]
        public void Print_Warning_When_Overriding_File() => LucidTest
            .Arrange(() =>
            {
                const string OutputDirectory = @"C:\output";
                const string FileName = "file.md";

                var fullPath = CombinePath(OutputDirectory, FileName);
                FileSystem.AddFile(fullPath, new MockFileData("content"));

                var file = new MarkdownFile(FileName, fullPath, FileName);
                return new
                {
                    Container = new OutputContainer(FileSystem, Logger, OutputDirectory),
                    File = file
                };
            })
            .Act(param => param.Container.Save(param.File))
            .Assert(() => Logger.Received().Log(Arg.Is<string>(message => message.ToLower().Contains("warning"))));

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
                    Container = new OutputContainer(FileSystem, Logger, values.OutputDirectory),
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
