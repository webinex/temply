---
title: Localization
sidebar_position: 4
---

# Localization

Temply supports following localization types:

- Key
- Resource
- Custom

## Key Localization

Key localization allows you to create multiple templates for each localization.  

Create your custom implementation of `ITemplyKeyReplacer`

```csharp
internal class MyKeyReplacer : ITemplyKeyReplacer
{
    private UserLangService _userLangService;

    public Task<string> ReplaceAsync(TemplyArgs args)
    {
        var newKey = $"{args.Source.Value}_{_userLangService.Lang}";
        return Task.FromResult(newKey);
    }
}
```

And register it

```csharp
services.AddTemply(x => x
    .AddProfile(p => p
        .Add("hello_en", "Hello!")
        .Add("hello_fr", "Salut!"))
    .AddKeyReplacer<MyKeyReplacer>());
```

## Resource Localization

Resource localization allows you to use localized string for each localization.  
Like: `We say: {{ localize "hello" values.name }}` to print `We say: Hello John` for `en` locale and `We say: Salut John` for `fr` locale.  

Create your custom implementation of `ITemplyLocalizationService`

```csharp
public class LocalizationService : ITemplyLocalizationService
{
    private readonly UserLangService _langService;
    
    private readonly IDictionary<string, IDictionary<string, string>> _values =
        new Dictionary<string, IDictionary<string, string>>
        {
            ["en"] = new Dictionary<string, string>
            {
                ["hello"] = "Hello {0}"
            },
            ["fr"] = new Dictionary<string, string>
            {
                ["hello"] = "Salut {0}"
            }
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
```

And register it

```csharp
services.AddTemply(x => x
    .AddLocalization<LocalizationService>()))
```

It will expose `localize` function which would be always accessible in your templates

## Custom Localization

See [Configure Scriban](./advanced-guides/configure-scriban) section
