using MarkdownCodeEmbed.Model;
using System.Text.RegularExpressions;

namespace MarkdownCodeEmbed.Container
{
    internal class CodeContainer
    {
        private static readonly Regex EmbedCodeDirectiveRegex = new Regex(@"\[ *embed-code *\] *: *# *\(.*\)", RegexOptions.IgnoreCase);

        public MarkdownFile EmbedCode(MarkdownFile inputFile)
        {
            var content = EmbedCodeDirectiveRegex.Replace(inputFile.Content, InsertCode);
            return new MarkdownFile(inputFile.FileName, inputFile.RelativePath, inputFile.RelativePath)
            {
                Content = content
            };
        }

        private string InsertCode(Match embedCodeDirective)
        {
            return @"```csharp
public void TestCodeEmbed() { }
```";
        }
    }
}
