using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Webinex.Temply
{
    /// <summary>
    ///     Gives an ability to replace key based on arguments.
    ///     Such as localization or different authorization rights.
    /// </summary>
    public interface ITemplyKeyReplacer
    {
        /// <summary>
        ///     Returns new key
        /// </summary>
        /// <param name="args">Temply args</param>
        /// <returns>New key</returns>
        Task<string> ReplaceAsync([NotNull] TemplyArgs args);
    }

    internal class EmptyKeyReplacer : ITemplyKeyReplacer
    {
        public Task<string> ReplaceAsync(TemplyArgs args)
        {
            args = args ?? throw new ArgumentNullException(nameof(args));
            if (!args.Source.IsKeyed) throw new ArgumentException($"Might be keyed {nameof(TemplySource)}");
            
            return Task.FromResult(args.Source.Value);
        }
    }
}