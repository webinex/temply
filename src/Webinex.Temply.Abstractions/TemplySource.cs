using System;
using System.Diagnostics.CodeAnalysis;

namespace Webinex.Temply
{
    /// <summary>
    ///     Describes template source to be used in rendering.
    ///     It can be key of template specifies in TemplyProfile or template text representation.
    /// </summary>
    public class TemplySource
    {
        private const string KEY_PREFIX = "key://";
        
        private TemplySource([NotNull] string value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            if (value.StartsWith(KEY_PREFIX))
            {
                Value = value.Substring(KEY_PREFIX.Length);
                IsKeyed = true;
            }
            else
            {
                Value = value;
                IsKeyed = false;
            }
        }

        /// <summary>
        ///     When IsKeyed true - Key of template.  
        ///     When IsKeyed false - template text representation.  
        /// </summary>
        [NotNull]
        public string Value { get; }

        /// <summary>
        ///     When true - Value is key.
        ///     When false - Value is template string representation.  
        /// </summary>
        public bool IsKeyed { get; }

        /// <summary>
        ///     Creates new keyed temply source with given key.
        /// </summary>
        /// <param name="key">Template key</param>
        /// <returns><see cref="TemplySource"/></returns>
        public static TemplySource Keyed([NotNull] string key)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            return new TemplySource(KEY_PREFIX + key);
        }

        /// <summary>
        ///     Creates new text representation temply source with given text.
        /// </summary>
        /// <param name="text">Text template representation</param>
        /// <returns><see cref="TemplySource"/></returns>
        public static TemplySource Text([NotNull] string text)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            return new TemplySource(text);
        }
    }
}