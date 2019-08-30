using CommandLine;
using MarkdownCodeEmbed.Logger;
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
            var container = new MainContainer();
            var logger = container.Resolve<ILogger>();
            if (logger is null)
            {
                throw new Exception("Can't resolve instance of " + nameof(ILogger));
            }

            logger.Log($"Welcome to {nameof(MarkdownCodeEmbed)} tool!");
            logger.Log("Version: " + Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            args.Print();

            var application = container.Resolve<Application>();
            application.Run(args);
        }
    }
}
