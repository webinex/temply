using System.IO;
using NUnit.Framework;
using Shouldly;
using Webinex.Temply.Tests.E2e.Templates;

namespace Webinex.Temply.Tests.E2e;

public class JsonAsPlainTextSourceTests : BaseE2eTests
{
    [Test]
    public void ShouldBeOk()
    {
        var expectedResult = $@"{{
  ""{TemplatesInfo.Keys.JsonTemplate1}"": ""{TemplatesInfo.ExpectedResult.JsonTemplate1}"",
  ""{TemplatesInfo.Keys.JsonTemplate2}"": ""{TemplatesInfo.ExpectedResult.JsonTemplate2}""
}}";

        Render("JsonTemplates").ShouldBe(expectedResult);
    }

    protected override void ConfigureTemply(ITemplyConfiguration configuration)
    {
        configuration.AddProfile(x => x.AddFile(Path.Combine(TemplatesInfo.Directory, "JsonTemplates.json")));
    }
}