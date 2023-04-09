using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Webinex.Temply.Resources
{
    public class TemplyResource : ITemplyResourceLoader
    {
        private TemplyResource(string key, ITemplyTextSource textSource)
        {
            Key = key;
            TextSource = textSource;
        }

        public static TemplyResource File([NotNull] string key, [NotNull] string path, bool noCache = false)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            path = path ?? throw new ArgumentNullException(nameof(path));

            return new TemplyResource(key, new FileTemplyTextSource(path, noCache));
        }

        public static TemplyResource Memory([NotNull] string key, [NotNull] string value)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            value = value ?? throw new ArgumentNullException(nameof(value));

            return new TemplyResource(key, new MemoryTemplyTextSource(value));
        }

        [NotNull]
        public string Key { get; }
        
        [NotNull]
        public ITemplyTextSource TextSource { get; }

        public async Task<TemplyResourceResult> ByKeyAsync(string key)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));

            return key == Key
                ? TemplyResourceResult.Succeed(await TextSource.ReadAsync())
                : TemplyResourceResult.NotFound();
        }
    }
}