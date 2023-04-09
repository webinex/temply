using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Webinex.Temply.Resources
{
    public class YamlTemplyResource : ITemplyResourceLoader
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private IDictionary<string, string> _result;

        public YamlTemplyResource([NotNull] string path, bool noCache = false)
            : this(new FileTemplyTextSource(path), noCache)
        {
        }

        public YamlTemplyResource([NotNull] ITemplyTextSource textSource, bool noCache = false)
        {
            TextSource = textSource ?? throw new ArgumentNullException(nameof(textSource));
            NoCache = noCache;
        }

        [NotNull] public ITemplyTextSource TextSource { get; }
        public bool NoCache { get; }

        public async Task<TemplyResourceResult> ByKeyAsync(string key)
        {
            var values = new Reader(await TextSource.ReadAsync()).Read();

            return values.TryGetValue(key, out var value)
                ? TemplyResourceResult.Succeed(value)
                : TemplyResourceResult.NotFound();
        }

        private async Task<IDictionary<string, string>> ReadAsync()
        {
            if (!NoCache)
                return new Reader(await TextSource.ReadAsync()).Read();

            if (_result != null)
                return _result;

            await _semaphore.WaitAsync();
            try
            {
                if (_result != null)
                    return _result;

                _result = new Reader(await TextSource.ReadAsync()).Read();
                return _result;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private class Reader
        {
            public Reader(string text)
            {
                Text = text;
            }

            private string Text { get; }
            private IDictionary<string, object> Values { get; set; }
            private IDictionary<string, string> Result { get; } = new Dictionary<string, string>();

            public IDictionary<string, string> Read()
            {
                Values = new Deserializer().Deserialize<IDictionary<string, object>>(Text);
                Visit(null, Values);
                return Result;
            }

            private void Visit(string path, object value)
            {
                switch (value)
                {
                    case IDictionary dictionary:
                    {
                        foreach (DictionaryEntry entry in dictionary)
                        {
                            var prefix = path != null ? $"{path}." : string.Empty;
                            var newPath = $"{prefix}{entry.Key}";
                            Visit(newPath, entry.Value);
                        }
                        
                        break;
                    }

                    case string stringValue:
                    {
                        Result.Add(path, stringValue);
                        break;
                    }

                    default:
                        throw new InvalidOperationException($"Unexpected value type: {value.GetType().Name}");
                }
            }
        }
    }
}