using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace Webinex.Temply.Resources
{
    public class JsonTemplyResource : ITemplyResourceLoader
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private IDictionary<string, string> _result;

        public JsonTemplyResource([NotNull] string path, bool noCache = false)
            : this(new FileTemplyTextSource(path), noCache)
        {
        }

        public JsonTemplyResource([NotNull] ITemplyTextSource textSource, bool noCache = false)
        {
            TextSource = textSource ?? throw new ArgumentNullException(nameof(textSource));
            NoCache = noCache;
        }

        [NotNull] public ITemplyTextSource TextSource { get; }

        public bool NoCache { get; }

        public async Task<TemplyResourceResult> ByKeyAsync(string key)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            var values = await ReadFlattenAsync();

            return values.TryGetValue(key, out var value)
                ? TemplyResourceResult.Succeed(value)
                : TemplyResourceResult.NotFound();
        }

        private async Task<IDictionary<string, string>> ReadFlattenAsync()
        {
            if (!NoCache)
                return await ReadFlattenInternalAsync();

            if (_result != null)
                return _result;

            await _semaphore.WaitAsync();
            try
            {
                if (_result != null)
                    return _result;
                
                _result = await ReadFlattenInternalAsync();
                return _result;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task<IDictionary<string, string>> ReadFlattenInternalAsync()
        {
            var text = await TextSource.ReadAsync();
            var jsonNode = JsonNode.Parse(text);
            var values = Read(jsonNode);
            return new Dictionary<string, string>(values);
        }

        private IEnumerable<KeyValuePair<string, string>> Read(JsonNode node)
        {
            switch (node)
            {
                case JsonObject jsonObject:
                {
                    return jsonObject
                        .Where(x => x.Value != null)
                        .SelectMany(x => Read(x.Value));
                }

                case JsonValue jsonValue:
                {
                    var value = jsonValue.ToString();
                    var key = jsonValue.GetPath().Substring("$.".Length);
                    return new[] { new KeyValuePair<string, string>(key, value) };
                }

                default:
                    throw new InvalidOperationException($"Invalid json node kind = {node.GetType().Name}");
            }
        }
    }
}