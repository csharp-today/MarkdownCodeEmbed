namespace MarkdownCodeEmbed.Model
{
    internal class MarkdownFile
    {
        public string Content { get; set; }
        public string FileName { get; }
        public string FullPath { get; }
        public string RelativePath { get; }

        public MarkdownFile(string fileName, string fullPath, string relativePath)
        {
            FileName = fileName;
            FullPath = fullPath;
            RelativePath = relativePath;
        }

        public override string ToString() => RelativePath;
    }
}
