using LucidCode;
using Xunit;
using Unity;
using Shouldly;

namespace MarkdownCodeEmbed.Test
{
    public class MainContainerTest
    {
        [Fact]
        public void Create_Application() => LucidTest
            .Act(() => new MainContainer().Resolve<Application>())
            .Assert(application => application.ShouldNotBeNull());
    }
}
