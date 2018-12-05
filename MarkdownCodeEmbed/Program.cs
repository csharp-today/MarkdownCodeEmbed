using System;
using System.Reflection;

namespace MarkdownCodeEmbed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Welcome to {nameof(MarkdownCodeEmbed)} tool!");
            Console.WriteLine("Version: " + Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
        }
    }
}
