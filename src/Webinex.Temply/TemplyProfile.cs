using System.Diagnostics.CodeAnalysis;

namespace Webinex.Temply
{
    public abstract class TemplyProfile
    {
        public abstract void Configure([NotNull] TemplyProfileBuilder builder);
    }
}