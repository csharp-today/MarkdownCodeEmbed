using System;

namespace MarkdownCodeEmbed.Logger
{
    internal class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine(message);
    }
}
