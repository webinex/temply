using NUnit.Framework;
using Shouldly;

namespace Webinex.Temply.Tests.E2e;

public class TextSourceTests : BaseE2eTests
{
    [Test]
    public void ShouldBeOk()
    {
        var source = TemplySource.Text("Hello {{ values.name }}");
        var result = Temply.RenderAsync(new TemplyArgs(source, new { name = "John" }))
            .GetAwaiter().GetResult();

        result.ShouldBe("Hello John");
    }
}