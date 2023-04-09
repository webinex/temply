using System.IO;
using NUnit.Framework;
using Shouldly;
using Webinex.Temply.Tests.E2e.Templates;

namespace Webinex.Temply.Tests.E2e;

public class WhenJsonFileSource : BaseE2eTests
{
    [Test]
    public void ShouldBeOk()
    {
        Render(TemplatesInfo.Keys.JsonTemplate1).ShouldBe(TemplatesInfo.ExpectedResult.JsonTemplate1);
        Render(TemplatesInfo.Keys.JsonTemplate2).ShouldBe(TemplatesInfo.ExpectedResult.JsonTemplate2);
    }

    protected override void ConfigureTemply(ITemplyConfiguration configuration)
    {
        configuration.AddProfile(x => x.AddJson(Path.Combine(TemplatesInfo.Directory, "JsonTemplates.json")));
    }
}