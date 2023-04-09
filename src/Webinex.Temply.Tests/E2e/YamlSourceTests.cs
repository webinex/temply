using System.IO;
using NUnit.Framework;
using Shouldly;
using Webinex.Temply.Tests.E2e.Templates;

namespace Webinex.Temply.Tests.E2e;

public class YamlSourceTests : BaseE2eTests
{
    [Test]
    public void ShouldBeOk()
    {
        Render(TemplatesInfo.Keys.YamlTemplate1).ShouldBe(TemplatesInfo.ExpectedResult.YamlTemplate1);
        Render(TemplatesInfo.Keys.YamlTemplate2).ShouldBe(TemplatesInfo.ExpectedResult.YamlTemplate2);
    }

    protected override void ConfigureTemply(ITemplyConfiguration configuration)
    {
        configuration.AddProfile(x => x.AddYaml(Path.Combine(TemplatesInfo.Directory, "YamlTemplates.yaml")));
    }
}