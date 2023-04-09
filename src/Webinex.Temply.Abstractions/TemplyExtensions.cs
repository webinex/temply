using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Webinex.Temply
{
    public static class TemplyExtensions
    {
        /// <summary>
        ///     Renders template associated with given <paramref name="key"/>
        /// </summary>
        /// <param name="temply"><see cref="ITemply"/></param>
        /// <param name="key">Template key</param>
        /// <returns>Rendered text result</returns>
        public static Task<string> RenderAsync(
            [NotNull] this ITemply temply,
            [NotNull] string key)
        {
            temply = temply ?? throw new ArgumentNullException(nameof(temply));
            key = key ?? throw new ArgumentNullException(nameof(key));

            return RenderAsync(temply, key, null);
        }

        /// <summary>
        ///     Renders template associated with given <paramref name="key"/>
        /// </summary>
        /// <param name="temply"><see cref="ITemply"/></param>
        /// <param name="key">Template key</param>
        /// <param name="values">Values accessible in template</param>
        /// <returns>Rendered text result</returns>
        public static Task<string> RenderAsync(
            [NotNull] this ITemply temply,
            [NotNull] string key,
            [MaybeNull] object values)
        {
            temply = temply ?? throw new ArgumentNullException(nameof(temply));
            key = key ?? throw new ArgumentNullException(nameof(key));

            var source = TemplySource.Keyed(key);
            return temply.RenderAsync(new TemplyArgs(source, values));
        }
        /// <summary>
        ///     Renders template associated with given <paramref name="text"/>
        /// </summary>
        /// <param name="temply"><see cref="ITemply"/></param>
        /// <param name="text">Template text representation</param>
        /// <returns>Rendered text result</returns>
        public static Task<string> RenderTextAsync(
            [NotNull] this ITemply temply,
            [NotNull] string text)
        {
            temply = temply ?? throw new ArgumentNullException(nameof(temply));
            text = text ?? throw new ArgumentNullException(nameof(text));

            return RenderTextAsync(temply, text, null);
        }

        /// <summary>
        ///     Renders template associated with given <paramref name="text"/>
        /// </summary>
        /// <param name="temply"><see cref="ITemply"/></param>
        /// <param name="text">Template text representation</param>
        /// <param name="values">Values accessible in template</param>
        /// <returns>Rendered text result</returns>
        public static Task<string> RenderTextAsync(
            [NotNull] this ITemply temply,
            [NotNull] string text,
            [MaybeNull] object values)
        {
            temply = temply ?? throw new ArgumentNullException(nameof(temply));
            text = text ?? throw new ArgumentNullException(nameof(text));

            var source = TemplySource.Text(text);
            return temply.RenderAsync(new TemplyArgs(source, values));
        }
    }
}