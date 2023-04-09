using System;
using System.Diagnostics.CodeAnalysis;

namespace Webinex.Temply.Resources
{
    public class TemplyResourceResult
    {
        private TemplyResourceResult(string value, bool success)
        {
            Value = value;
            Success = success;
        }
        
        public string Value { get; }
        
        public bool Success { get; }

        public static TemplyResourceResult Succeed([NotNull] string value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));
            return new TemplyResourceResult(value, true);
        }

        public static TemplyResourceResult NotFound()
        {
            return new TemplyResourceResult(null, false);
        }
    }
}