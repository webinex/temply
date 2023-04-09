using System;
using NUnit.Framework;
using Shouldly;
using Webinex.Temply.Resources;

namespace Webinex.Temply.Tests;

[TestFixture]
public class JsonTemplyResourceTests
{
    private string _source;
    private TemplyResourceResult _result;

    [Test]
    public void WhenString_ShouldGetCorrectly()
    {
        _source = @"
{
    ""v1"": {
        ""v1_1"": ""123""
    }
}";

        Run();

        _result.Success.ShouldBeTrue();
        _result.Value.ShouldBe("123");
    }

    [Test]
    public void WhenNumeric_ShouldGetCorrectly()
    {
        _source = @"
{
    ""v1"": {
        ""v1_1"": 123
    }
}";

        Run();

        _result.Success.ShouldBeTrue();
        _result.Value.ShouldBe("123");
    }

    [Test]
    public void WhenContainsCollection_ShouldThrow()
    {
        _source = @"
{
    ""v1"": [
        ""123""
    ]
}
";

        var exception = Assert.Throws<InvalidOperationException>(() => Run("v1[0]"));

        exception!.Message.Contains("Invalid json node kind").ShouldBeTrue();
    }

    [Test]
    public void WhenKeyNotFound_ShouldNotReturnSuccessResult()
    {
        _source = @"{ ""val"": ""val"" }";

        Run("val1");

        _result.Success.ShouldBeFalse();
    }

    private void Run(string key = "v1.v1_1")
    {
        var resource = new JsonTemplyResource(new MemoryTemplyTextSource(_source));
        _result = resource.ByKeyAsync(key).GetAwaiter().GetResult();
    }

    [SetUp]
    public void SetUp()
    {
        _source = null;
        _result = null;
    }
}