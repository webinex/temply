using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Webinex.Temply.Resources;

namespace Webinex.Temply.Tests;

[TestFixture]
internal class TemplyFacadeTests
{
    public class GetAsync : TemplyFacadeTests
    {
        private string _result;

        [Test]
        public void WhenValid_ShouldReturnResult()
        {
            Resource1 = TemplyResource.Memory("key1", "1111");
            Resource2 = TemplyResource.Memory("key2", "2222");

            Run("key1");

            _result.ShouldBe("1111");
        }

        [Test]
        public void WhenMultipleMatch_ShouldReturnFirst()
        {
            Resource1 = TemplyResource.Memory("key1", "1111");
            Resource2 = TemplyResource.Memory("key1", "2222");

            Run("key1");

            _result.ShouldBe("1111");
        }

        private void Run(string key = "123")
        {
            _result = Subject().TemplateByKeyAsync(key).GetAwaiter().GetResult();
        }

        [SetUp]
        public new void SetUp()
        {
            _result = null;
        }
    }

    protected ITemplyKeyReplacer KeyReplacer;
    protected Mock<ITemplateRenderer> RendererMock;

    protected ITemplyResourceLoader Resource1;
    protected ITemplyResourceLoader Resource2;

    protected IEnumerable<ITemplyResourceLoader> Resources =>
        new[] { Resource1, Resource2 }.Where(x => x != null).ToArray();

    protected TemplyFacade Subject()
    {
        return new TemplyFacade(Resources, KeyReplacer, RendererMock.Object);
    }

    [SetUp]
    public void SetUp()
    {
        KeyReplacer = null;
        Resource1 = null;
        Resource2 = null;

        RendererMock = new Mock<ITemplateRenderer>();
    }
}