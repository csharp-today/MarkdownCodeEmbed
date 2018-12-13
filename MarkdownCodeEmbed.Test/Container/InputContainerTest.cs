using LucidCode;
using MarkdownCodeEmbed.Container;
using Shouldly;
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
                const string Path = "root-path";
                FileSystem.AddFile(FileSystem.Path.Combine(Path, files[0]), data);
                FileSystem.AddFile(FileSystem.Path.Combine(Path, "subdir", files[1]), data);
                FileSystem.AddFile("other-path\\skip.md", data);
                FileSystem.AddFile(FileSystem.Path.Combine(Path, "not-markdown.txt"), data);
                return Path;
            })
            .Act(path => new InputContainer(FileSystem, path).GetMarkdownFiles())
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
    }
}
