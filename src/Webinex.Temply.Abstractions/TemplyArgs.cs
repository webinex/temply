using System;
using System.Diagnostics.CodeAnalysis;

namespace Webinex.Temply
{
    /// <summary>
    ///     Specifies template source and values which might be used in template
    /// </summary>
    public class TemplyArgs
    {
        public TemplyArgs([NotNull] TemplyArgs args)
        {
            args = args ?? throw new ArgumentNullException(nameof(args));
            Source = args.Source;
            Values = args.Values ?? new object();
        }

        public TemplyArgs([NotNull] string key, object values = null)
            : this(TemplySource.Keyed(key), values)
        {
        }

        public TemplyArgs([NotNull] TemplySource source, object values = null)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Values = values ?? new object();
        }

        /// <summary>
        ///     Template source
        /// </summary>
        [NotNull] public TemplySource Source { get; private set; }

        /// <summary>
        ///     Values accessible in template
        /// </summary>
        [NotNull] public object Values { get; }

        /// <summary>
        ///     Creates copy of this args with given template key
        /// </summary>
        /// <param name="key">New key</param>
        /// <returns><see cref="TemplyArgs"/></returns>
        public TemplyArgs WithKey([NotNull] string key)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));

            return new TemplyArgs(this)
            {
                Source = TemplySource.Keyed(key),
            };
        }
    }
}