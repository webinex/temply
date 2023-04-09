using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace Webinex.Temply.Tests.E2e;

public class LocalizationServiceTests
{
    private IServiceProvider _serviceProvider;

    [Test]
    public void WhenFormatted_ShouldBeOk()
    {
        var enResult = Render("formatted", "en");
        enResult.ShouldBe("It's how we say hello: Hello John");

        var frResult = Render("formatted", "fr");
        frResult.ShouldBe("It's how we say hello: Salut John");
    }

    [Test]
    public void WhenNotFormatted_ShouldBeOk()
    {
        var enResult = Render("not-formatted", "en");
        enResult.ShouldBe("It's how we say hello: Hello");

        var frResult = Render("not-formatted", "fr");
        frResult.ShouldBe("It's how we say hello: Salut");
    }

    private string Render(string key, string lang)
    {
        using var scope = _serviceProvider.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var userLangService = serviceProvider.GetRequiredService<UserLangService>();
        userLangService.Lang = lang;
        var temply = serviceProvider.GetRequiredService<ITemply>();

        return temply.RenderAsync(new TemplyArgs(key, new { name = "John" })).GetAwaiter().GetResult();
    }

    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection()
            .AddScoped<UserLangService>()
            .AddTemply(temply => temply
                .AddProfile(x =>
                {
                    x.Add("formatted", "It's how we say hello: {{ localize \"formatted-hello\" values.name }}");
                    x.Add("not-formatted", "It's how we say hello: {{ localize \"not-formatted-hello\" }}");
                })
                .AddLocalization<LocalizationService>());

        _serviceProvider = services.BuildServiceProvider();
    }

    private class UserLangService
    {
        public string Lang { get; set; }
    }

    private class LocalizationService : ITemplyLocalizationService
    {
        private readonly UserLangService _langService;

        private readonly IDictionary<string, IDictionary<string, string>> _values =
            new Dictionary<string, IDictionary<string, string>>
            {
                ["en"] = new Dictionary<string, string>
                {
                    ["formatted-hello"] = "Hello {0}",
                    ["not-formatted-hello"] = "Hello",
                },
                ["fr"] = new Dictionary<string, string>
                {
                    ["formatted-hello"] = "Salut {0}",
                    ["not-formatted-hello"] = "Salut",
                },
            };

        public LocalizationService(UserLangService langService)
        {
            _langService = langService;
        }

        public string Get(string key, params object[] values)
        {
            return string.Format(_values[_langService.Lang][key], values);
        }
    }
}