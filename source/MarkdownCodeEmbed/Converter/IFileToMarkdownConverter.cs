namespace MarkdownCodeEmbed.Converter
{
    interface IFileToMarkdownConverter
    {
        string GetMarkdown(string filePath, string fileContent);
    }
}
