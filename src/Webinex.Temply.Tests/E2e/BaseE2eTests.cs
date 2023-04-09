using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Webinex.Temply.Tests.E2e.Templates;

namespace Webinex.Temply.Tests.E2e;

public class BaseE2eTests
{
    protected ITemply Temply { get; private set; }

    protected string Render(string key)
    {
        return Temply.RenderAsync(new TemplyArgs(key, TemplatesInfo.Values)).GetAwaiter().GetResult();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }

    protected virtual void ConfigureTemply(ITemplyConfiguration configuration)
    {
    }

    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);

        services.AddTemply(ConfigureTemply);

        Temply = services.BuildServiceProvider().GetRequiredService<ITemply>();
    }
}