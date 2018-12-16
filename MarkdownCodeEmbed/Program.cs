using CommandLine;
using System;
using System.Reflection;
using Unity;

namespace MarkdownCodeEmbed
{
    public class Program
    {
        public static void Main(string[] args) => Parser.Default
            .ParseArguments<InputArgs>(args)
            .WithParsed(inputArgs => new Program().Run(inputArgs));

        public void Run(InputArgs args)
        {
            Console.WriteLine($"Welcome to {nameof(MarkdownCodeEmbed)} tool!");
            Console.WriteLine("Version: " + Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            args.Print();

            var application = new MainContainer().Resolve<Application>();
            application.Run(args);
        }
    }
}
