using System;
using NUnit.Framework;
using Shouldly;
using Webinex.Temply.Resources;

namespace Webinex.Temply.Tests;

[TestFixture]
public class YamlTemplyResourceTests
{
    private string _yaml;
    private TemplyResourceResult _result;

    [Test]
    public void WhenString_ShouldGetCorrectly()
    {
        _yaml = @"
val_1:
    val_1_1: value
";

        Run();

        _result.Success.ShouldBeTrue();
        _result.Value.ShouldBe("value");
    }

    [Test]
    public void WhenNumeric_ShouldGetCorrectly()
    {
        _yaml = @"
val_1:
    val_1_1: 1
";
        Run();

        _result.Success.ShouldBeTrue();
        _result.Value.ShouldBe("1");
    }

    [Test]
    public void WhenBoolean_ShouldGetCorrectly()
    {
        _yaml = @"
val_1:
    val_1_1: true
";
        Run();

        _result.Success.ShouldBeTrue();
        _result.Value.ShouldBe("true");
    }

    [Test]
    public void WhenContainsArray_ShouldThrow()
    {
        _yaml = @"
val_1:
  - 123
  - 321
";

        Assert.Throws<InvalidOperationException>(() => Run());
    }

    [Test]
    public void WhenNotFound_ShouldReturnNotSuccessResult()
    {
        _yaml = "val1: val";

        Run("val2");

        _result.Success.ShouldBeFalse();
    }

    private void Run(string key = "val_1.val_1_1")
    {
        var resource = new YamlTemplyResource(new MemoryTemplyTextSource(_yaml));
        _result = resource.ByKeyAsync(key).GetAwaiter().GetResult();
    }

    [SetUp]
    public void SetUp()
    {
        _result = null;
        _yaml = null;
    }
}