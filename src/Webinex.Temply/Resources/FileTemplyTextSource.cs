using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Webinex.Temply.Resources
{
    public class FileTemplyTextSource : ITemplyTextSource
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private string _result;
        
        public FileTemplyTextSource([NotNull] string path, bool noCache = false)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            NoCache = noCache;

            if (!System.IO.Path.IsPathFullyQualified(path))
                throw new ArgumentException("Might be fully qualified", nameof(path));
        }

        public string Path { get; }
        
        public bool NoCache { get; }
        
        public async Task<string> ReadAsync()
        {
            if (NoCache)
                return await ReadAsTextInternalAsync();

            if (_result != null)
                return _result;

            await _semaphore.WaitAsync();
            try
            {
                if (_result != null)
                    return _result;

                _result = await ReadAsTextInternalAsync();
                return _result;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private Task<string> ReadAsTextInternalAsync()
        {
            return File.ReadAllTextAsync(Path, Encoding.UTF8);
        }
    }
}