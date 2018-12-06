using CommandLine;
using System;

namespace MarkdownCodeEmbed
{
    public class InputArgs
    {
        [Option("input", Required = true, HelpText = "Input markdown directory for processing.")]
        public string InputDirectory { get; set; }

        [Option("output", Required = true, HelpText = "Output directory for processed markdown.")]
        public string OutputDirectory { get; set; }

        [Option("code", Required = true, HelpText = "Directory with source code.")]
        public string SourceCodeDirectory { get; set; }

        public void Print()
        {
            Console.WriteLine($"{nameof(InputDirectory)}: {InputDirectory}");
            Console.WriteLine($"{nameof(SourceCodeDirectory)}: {SourceCodeDirectory}");
            Console.WriteLine($"{nameof(OutputDirectory)}: {OutputDirectory}");
        }
    }
}
