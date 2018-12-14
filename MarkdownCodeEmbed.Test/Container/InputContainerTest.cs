using LucidCode;
using MarkdownCodeEmbed.Container;
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
            .DefineExpected(new[] { "file1.md", "second-file.md" })
            .Arrange(files =>
            {
                var data = new MockFileData("");
                const string Path = @"C:\root-path";
                FileSystem.AddFile(FileSystem.Path.Combine(Path, files[0]), data);
                FileSystem.AddFile(FileSystem.Path.Combine(Path, "subdir", files[1]), data);
                FileSystem.AddFile("other-path\\skip.md", data);
                FileSystem.AddFile(FileSystem.Path.Combine(Path, "not-markdown.txt"), data);
                return Path;
            })
            .Act(GetMarkdownFiles)
            .Assert((expectedFileNames, files) =>
            {
                files.ShouldNotBeNull();
                var fileNames = files.Select(fullPath => FileSystem.Path.GetFileName(fullPath)).ToArray();
                fileNames.Length.ShouldBe(2);
                foreach(var expectedFileName in expectedFileNames)
                {
                    fileNames.Contains(expectedFileName).ShouldBeTrue();
                }
            });

        [Fact]
        public void Get_Relative_File_Paths() => LucidTest
            .DefineExpected(new
            {
                Files = new[] { "doc.md", "other.md" },
                Path = "sub-directory-name"
            })
            .Arrange(expected =>
            {
                const string RootDirectory = @"C:\some\dir\root-directory";
                var data = new MockFileData("");
                FileSystem.AddFile(FileSystem.Path.Combine(RootDirectory.ToUpper(), expected.Path, expected.Files[0]), data);
                FileSystem.AddFile(FileSystem.Path.Combine(RootDirectory, expected.Path, "one-more-sub-dir", expected.Files[1]), data);
                return RootDirectory;
            })
            .Act(GetMarkdownFiles)
            .Assert((expected, files) =>
            {
                files.ShouldNotBeNull();
                var fileNamesWithFullPaths = files.Select(fullPath => (name: FileSystem.Path.GetFileName(fullPath), fullPath)).ToArray();

                fileNamesWithFullPaths.Length.ShouldBe(expected.Files.Length);
                foreach (var expectedFile in expected.Files)
                {
                    var path = fileNamesWithFullPaths.First(f => f.name == expectedFile);
                    path.fullPath.StartsWith(expected.Path).ShouldBeTrue();
                }
            });

        private IEnumerable<string> GetMarkdownFiles(string path) => new InputContainer(FileSystem, path).GetMarkdownFiles();
    }
}
