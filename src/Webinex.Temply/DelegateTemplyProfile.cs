using System;
using System.Diagnostics.CodeAnalysis;

namespace Webinex.Temply
{
    internal class DelegateTemplyProfile : TemplyProfile
    {
        private readonly Action<TemplyProfileBuilder> _configure;

        public DelegateTemplyProfile([NotNull] Action<TemplyProfileBuilder> configure)
        {
            _configure = configure ?? throw new ArgumentNullException(nameof(configure));
        }

        public override void Configure(TemplyProfileBuilder builder)
        {
            _configure(builder);
        }
    }
}