using NUnit.Framework;
using Shouldly;
using Webinex.Temply.Tests.E2e.Templates;

namespace Webinex.Temply.Tests.E2e;

public class DirSourceTests : BaseE2eTests
{
    [Test]
    public void Json_ShouldBeOk()
    {
        Render(TemplatesInfo.Keys.JsonTemplate1).ShouldBe(TemplatesInfo.ExpectedResult.JsonTemplate1);
        Render(TemplatesInfo.Keys.JsonTemplate2).ShouldBe(TemplatesInfo.ExpectedResult.JsonTemplate2);
    }

    [Test]
    public void Yaml_ShouldBeOk()
    {
        Render(TemplatesInfo.Keys.YamlTemplate1).ShouldBe(TemplatesInfo.ExpectedResult.YamlTemplate1);
        Render(TemplatesInfo.Keys.YamlTemplate2).ShouldBe(TemplatesInfo.ExpectedResult.YamlTemplate2);
    }

    [Test]
    public void TxtTemplate_ShouldBeOk()
    {
        Render(TemplatesInfo.Keys.TxtTemplate).ShouldBe(TemplatesInfo.ExpectedResult.TxtTemplate);
    }

    protected override void ConfigureTemply(ITemplyConfiguration configuration)
    {
        configuration.AddProfile(x => x.AddDir(TemplatesInfo.Directory));
    }
}