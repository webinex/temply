using System.IO;
using NUnit.Framework;
using Shouldly;
using Webinex.Temply.Tests.E2e.Templates;

namespace Webinex.Temply.Tests.E2e;

public class TxtFileSourceTests : BaseE2eTests
{
    [Test]
    public void ShouldBeOk()
    {
        Render(TemplatesInfo.Keys.TxtTemplate).ShouldBe(TemplatesInfo.ExpectedResult.TxtTemplate);
    }

    protected override void ConfigureTemply(ITemplyConfiguration configuration)
    {
        configuration.AddProfile(x => x.AddFile(Path.Combine(TemplatesInfo.Directory, "TxtTemplate.txt")));
    }
}