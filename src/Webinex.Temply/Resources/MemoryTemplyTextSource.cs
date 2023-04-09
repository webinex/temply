using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Webinex.Temply.Resources
{
    public class MemoryTemplyTextSource : ITemplyTextSource
    {
        public MemoryTemplyTextSource([NotNull] string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        [NotNull]
        public string Value { get; }
        
        public Task<string> ReadAsync()
        {
            return Task.FromResult(Value);
        }
    }
}