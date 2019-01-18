namespace MarkdownCodeEmbed.Converter
{
    internal class FileToMarkdownConverter : IFileToMarkdownConverter
    {
        public string GetMarkdown(string filePath, string fileContent)
        {
            return $@"```csharp
{fileContent}
```";
        }
    }
}
