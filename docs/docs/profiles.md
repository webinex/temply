---
sidebar_position: 3
title: Profiles
---

# Profiles

## Create & Register

:::info Key duplication
You might be aware, if you add same key (in one or different profiles), first added template would be used.
:::info

Temply allows you to define profiles as:

### Profile Type

Profile might have parameterless constructor.

```csharp
public class MyTemplyProfile : TemplyProfile
{
    public override void Configure(TemplyProfileBuilder builder)
    {
      // ...
    }
}

services.AddTemply(x => x.AddProfile<MyTemplyProfile>());
```

### Profile Instance

```csharp
public class MyTemplyProfile : TemplyProfile
{
    private readonly IWebHostEnvironment _environment;

    public MyTemplyProfile(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public override void Configure(TemplyProfileBuilder builder)
    {
      // ...
    }
}

services.AddTemply(x => x.AddProfile(new MyTemplyProfile(_environment)));
```

### Profile Delegate

```csharp
services.AddTemply(x => x.AddProfile(profile => /* .... */));
```

## Builder

Profile builder allows you to configure your template. It has following methods:

- **Add** - adds in memory resource
- **AddFile** - adds file content as resource (when template key not specified, uses file name without extension as Template key)
- **AddYaml** - adds YAML entries as resources

  ```yaml
  users:
    hello: Hello {{ values.name }}
    bye: Bye {{ values.name }}
  ```

  would be added as `users.hello` and `users.bye` template keys

- **AddJson** - adds JSON entries as resources

  ```json
  {
    "users": {
      "hello": "Hello {{ values.name }}",
      "bye": "Bye {{ values.name }}"
    }
  }
  ```

  would be added as `users.hello` and `users.bye` template keys

- **AddDir** - adds directory files (not recursive) as templates

  `.html, .htm, .txt, .md` - files added using `AddFile` call  
   `.yaml, .yml` - files added using `AddYaml` call  
   `.json` - files added using `AddJson` call

  Note: `noCache` option would not affect newly added files (they would ignore)

- **Add(Resource Loader)** - adds built-in or custom implementation of resource loader
