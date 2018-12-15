using LucidCode;
using MarkdownCodeEmbed.Container;
using MarkdownCodeEmbed.Model;
using Shouldly;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Xunit;

namespace MarkdownCodeEmbed.Test.Container
{
    public class InputContainerTest : TestContainer
    {
        [Fact]
        public void Get_Markdown_Files_From_Specified_Directory() => LucidTest
            .DefineExpected(new
            {
                RootFileName = "file1.md",
                RootPath = @"C:\root-path",
                SubDirectoryFileName = "second-file.md",
                SubDirectoryName = "subdir"
            })
            .Arrange(expected =>
            {
                var data = new MockFileData("");
                FileSystem.AddFile(CombinePath(expected.RootPath, expected.RootFileName), data);
                FileSystem.AddFile(CombinePath(expected.RootPath, expected.SubDirectoryName, expected.SubDirectoryFileName), data);
                FileSystem.AddFile(@"other-path\skip.md", data);
                FileSystem.AddFile(CombinePath(expected.RootPath, "not-markdown.txt"), data);
                return expected.RootPath;
            })
            .Act(path => new InputContainer(FileSystem, path).GetMarkdownFiles())
            .Assert((expected, files) =>
            {
                files.ShouldNotBeNull();
                files.Count().ShouldBe(2);

                var rootFile = files.First(f => f.FileName == expected.RootFileName);
                rootFile.FullPath.ShouldBe(CombinePath(expected.RootPath, expected.RootFileName));
                rootFile.RelativePath.ShouldBe(expected.RootFileName);

                var subDirectoryFile = files.First(f => f.FileName == expected.SubDirectoryFileName);
                subDirectoryFile.FullPath.ShouldBe(CombinePath(expected.RootPath, expected.SubDirectoryName, expected.SubDirectoryFileName));
                subDirectoryFile.RelativePath.ShouldBe(CombinePath(expected.SubDirectoryName, expected.SubDirectoryFileName));
            });

        private string CombinePath(params string[] paths) => FileSystem.Path.Combine(paths);
    }
}
