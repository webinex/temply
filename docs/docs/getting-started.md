---
title: Getting Started
sidebar_position: 2
---

# Getting Started

## Install Nuget Package

```sh
dotnet add package Webinex.Temply
```

## Register Services

:::caution Snake Case
Be careful, Scriban automatically renames properties using snake_case.  
`FirstName` or `firstName` would be renamed to `first_name`
:::

```csharp
services
  .AddTemply(temply => temply
      .AddProfile(profile => {
          profile.Add("HelloWorld", "Hello, {{ values.first_name }} {{ values.last_name }}");
      }));
```

## Use It!

```csharp
public class KeyedTemplyExample
{
    private readonly ITemply _temply;

    public async Task PrintHelloAsync()
    {
        var values = new { firstName = "John", lastName = "Doe" };
        var text = await _temply.RenderAsync("HelloWorld", values);
        Console.WriteLine(text); // Prints: Hello, John Doe
    }
}

public class TextTemplyExample
{
    private readonly ITemply _temply;

    public async Task PrintHelloAsync()
    {
        var values = new { firstName = "John", lastName = "Doe" };
        var template = "Hello, {{ values.first_name }} {{ values.last_name }}";
        var text = await _temply.RenderTextAsync(template, values);
        Console.WriteLine(text); // Prints: Hello, John Doe
    }
}
```