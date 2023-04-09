---
sidebar_position: 1
title: Configure Scriban
---

# Configure Scriban

If you'd like to modify Scriban [TemplateContext](https://github.com/scriban/scriban/blob/master/src/Scriban/TemplateContext.cs), you can do it with `IPostConfigureScribanTemplateContext`.

For example:

- Add new shared function
- Add utility functions
- Add new shared values
- Populate user context
- Modify member renaming rules
- etc.

```csharp
public interface IPostConfigureScribanTemplateContext
{
    Task<TemplateContext> ConfigureAsync(TemplateContext context);
}
```

You can add multiple registrations of `IPostConfigureScribanTemplateContext`

## Example (Member Renamer)

:::caution
Be careful, built-in types would also be renamed by your renamer.
:::

```csharp title="Startup.cs"
services
    .AddTemply(x => x
        .AddPostConfigureScriban(context =>
        {
            context.MemberRenamer = (member) => member.Name
        }))
```

**OR**

```csharp title="MemberRenamerPostConfigureScribanTemplateContext.cs"
public class MemberRenamerPostConfigureScribanTemplateContext
{
    public Task<TemplateContext> ConfigureAsync(TemplateContext context)
    {
        context.MemberRenamer = (member) => member.Name;
        return Task.FromResult(context);
    }
}

```

```csharp title="Startup.cs"
services
    .AddTemply(x => x
        .AddPostConfigureScriban<MemberRenamerPostConfigureScribanTemplateContext>())
```