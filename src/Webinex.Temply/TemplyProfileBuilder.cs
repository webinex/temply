using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Webinex.Temply.Resources;

namespace Webinex.Temply
{
    public sealed class TemplyProfileBuilder
    {
        private readonly ITemplyConfiguration _configuration;

        internal TemplyProfileBuilder(ITemplyConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///     Adds <paramref name="values"/> as in memory sources
        /// </summary>
        /// <param name="values">Key-value templates</param>
        public TemplyProfileBuilder Add([NotNull] IDictionary<string, string> values)
        {
            values = values ?? throw new ArgumentNullException(nameof(values));

            foreach (var value in values)
                Add(value.Key, value.Value);

            return this;
        }

        /// <summary>
        ///     Adds <paramref name="value"/> as template for key <paramref name="key"/>
        /// </summary>
        /// <param name="key">Template key</param>
        /// <param name="value">Template representation</param>
        public TemplyProfileBuilder Add([NotNull] string key, [NotNull] string value)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            value = value ?? throw new ArgumentNullException(nameof(value));

            return Add(TemplyResource.Memory(key, value));
        }

        /// <summary>
        ///     Adds file from <paramref name="path"/> as template with key as file name without extension.
        /// </summary>
        /// <param name="path">Template file path</param>
        /// <param name="noCache">When false, file reads only once. When true - on every load.</param>
        /// <exception cref="ArgumentException">When <paramref name="path"/> not fully qualified path</exception>
        /// <exception cref="ArgumentException">When <paramref name="path"/> doesn't exist or not a file</exception>
        public TemplyProfileBuilder AddFile([NotNull] string path, bool noCache = false)
        {
            path = path ?? throw new ArgumentNullException(nameof(path));
            AssertFullyQualified(path, nameof(path));
            AssertFileExists(path, nameof(path));

            var key = Path.GetFileNameWithoutExtension(path);
            return AddFile(key, path, noCache);
        }

        /// <summary>
        ///     Adds file from <paramref name="path"/> as template for key <paramref name="key"/>
        /// </summary>
        /// <param name="path">Template file path</param>
        /// <param name="key">Template key</param>
        /// <param name="noCache">When false, file reads only once. When true - on every load.</param>
        /// <exception cref="ArgumentException">When <paramref name="path"/> not fully qualified path</exception>
        /// <exception cref="ArgumentException">When <paramref name="path"/> doesn't exist or not a file</exception>
        public TemplyProfileBuilder AddFile([NotNull] string key, [NotNull] string path, bool noCache = false)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            path = path ?? throw new ArgumentNullException(nameof(path));
            AssertFullyQualified(path, nameof(path));
            AssertFileExists(path, nameof(path));

            return Add(TemplyResource.File(key, path, noCache));
        }

        /// <summary>
        ///     Adds templates from YAML file.  
        ///     YAML file might contain only objects and scalar values.  
        ///     Object keys would be flattened with "." separator. Like:
        ///
        ///     users:  
        ///         invite:  
        ///             subject: Hello {{ values.name }}  
        ///
        ///     would be accessible via "users.invite.subject"
        /// </summary>
        /// <param name="path">YAML file path</param>
        /// <param name="noCache">When false - file reads only once. When true - on every load.</param>
        /// <exception cref="ArgumentException">When <paramref name="path"/> not fully qualified path</exception>
        /// <exception cref="ArgumentException">When <paramref name="path"/> doesn't exist or not a file</exception>
        public TemplyProfileBuilder AddYaml([NotNull] string path, bool noCache = false)
        {
            path = path ?? throw new ArgumentNullException(nameof(path));
            AssertFullyQualified(path, nameof(path));
            AssertFileExists(path, nameof(path));

            return Add(new YamlTemplyResource(path, noCache));
        }

        /// <summary>
        ///     Adds templates from JSON file.  
        ///     JSON file might contain only objects and scalar values.  
        ///     Object keys would be flattened with "." separator. Like:
        ///
        ///     {  
        ///         "users": {  
        ///             "invite": {  
        ///                 "subject": "Hello {{ values.name }}"  
        ///             }  
        ///         }  
        ///     }  
        ///
        ///     would be accessible via "users.invite.subject"
        /// </summary>
        /// <param name="path">JSON file path</param>
        /// <param name="noCache">When false - file reads only once. When true - on every load.</param>
        /// <exception cref="ArgumentException">When <paramref name="path"/> not fully qualified path</exception>
        /// <exception cref="ArgumentException">When <paramref name="path"/> doesn't exist or not a file</exception>
        public TemplyProfileBuilder AddJson([NotNull] string path, bool noCache = false)
        {
            path = path ?? throw new ArgumentNullException(nameof(path));
            AssertFullyQualified(path, nameof(path));
            AssertFileExists(path, nameof(path));

            return Add(new JsonTemplyResource(path, noCache));
        }

        /// <summary>
        ///     Adds files from directory (not recursive).  
        ///     `html, htm, md, txt` files loaded as plain text source.  
        ///     `yaml, yml` files loaded as YAML source.  
        ///     `json` files loaded as JSON source.
        ///
        ///     Note: <paramref name="noCache"/> doesn't affect newly added files. It's only passed in AddX noCache argument.
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <param name="extensions">| separated extensions</param>
        /// <param name="noCache">When true - file loads only once. Newly added files would not be tracked independent of <paramref name="noCache"/> value.</param>
        /// <exception cref="ArgumentException">Throws if path not fully qualified</exception>
        /// <exception cref="ArgumentException">Throws if directory doesn't exist or not a directory</exception>
        public TemplyProfileBuilder AddDir(
            [NotNull] string path,
            [NotNull] string extensions = "html|htm|md|txt|yaml|yml|json",
            bool noCache = false)
        {
            path = path ?? throw new ArgumentNullException(nameof(path));
            extensions = extensions ?? throw new ArgumentNullException(nameof(extensions));

            AssertFullyQualified(path, nameof(path));
            AssertDirectoryExists(path, nameof(path));

            return AddDir(path, new Regex($"^.*\\.({extensions})$"), noCache);
        }

        /// <summary>
        ///     Adds files from directory (not recursive).  
        ///     `html, htm, md, txt` files loaded as plain text source.  
        ///     `yaml, yml` files loaded as YAML source.  
        ///     `json` files loaded as JSON source.
        ///
        ///     Note: <paramref name="noCache"/> doesn't affect newly added files. It's only passed in AddX noCache argument.
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <param name="regex">Regex to check suitable files</param>
        /// <param name="noCache">When true - file loads only once. Newly added files would not be tracked independent of <paramref name="noCache"/> value.</param>
        /// <exception cref="ArgumentException">Throws if path not fully qualified</exception>
        /// <exception cref="ArgumentException">Throws if directory doesn't exist or not a directory</exception>
        public TemplyProfileBuilder AddDir([NotNull] string path, [NotNull] Regex regex, bool noCache = false)
        {
            path = path ?? throw new ArgumentNullException(nameof(path));
            regex = regex ?? throw new ArgumentNullException(nameof(regex));
            AssertFullyQualified(path, nameof(path));
            AssertDirectoryExists(path, nameof(path));

            var files = Directory.EnumerateFiles(path);
            var matched = files.Where(file => regex.IsMatch(Path.GetFileName(file))).ToArray();

            foreach (var matchedFilePath in matched)
                AddFileByExtension(matchedFilePath, noCache);

            return this;
        }

        private void AddFileByExtension([NotNull] string filePath, bool noCache)
        {
            filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            AssertFullyQualified(filePath, nameof(filePath));

            var extension = Path.GetExtension(filePath);

            switch (extension)
            {
                case ".html":
                case ".htm":
                case ".txt":
                case ".md":
                    var key = Path.GetFileNameWithoutExtension(filePath);
                    Add(TemplyResource.File(key, filePath, noCache));
                    break;

                case ".json":
                    Add(new JsonTemplyResource(filePath, noCache));
                    break;

                case ".yaml":
                case ".yml":
                    Add(new YamlTemplyResource(filePath, noCache));
                    break;

                default:
                    throw new InvalidOperationException($"Unknown file extension {extension}");
            }
        }

        /// <summary>
        ///     Adds <see cref="ITemplyResourceLoader"/>
        /// </summary>
        /// <param name="lifetime">Registration lifetime</param>
        /// <typeparam name="T"><see cref="ITemplyResourceLoader"/></typeparam>
        public TemplyProfileBuilder Add<T>(ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where T : ITemplyResourceLoader
        {
            return Add(typeof(T), lifetime);
        }

        /// <summary>
        ///     Adds <see cref="ITemplyResourceLoader"/>
        /// </summary>
        /// <param name="resourceLoaderType">Loader type. Might be assignable to <see cref="ITemplyResourceLoader"/></param>
        /// <param name="lifetime">Registration lifetime</param>
        /// <exception cref="ArgumentException">Throws if type not assignable to <see cref="ITemplyResourceLoader"/></exception>
        public TemplyProfileBuilder Add([NotNull] Type resourceLoaderType, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            resourceLoaderType = resourceLoaderType ?? throw new ArgumentNullException(nameof(resourceLoaderType));

            if (!typeof(ITemplyResourceLoader).IsAssignableFrom(resourceLoaderType))
            {
                throw new ArgumentException(
                    $"Might be assignable from {nameof(ITemplyResourceLoader)}",
                    nameof(resourceLoaderType));
            }

            _configuration.Services.Add(
                new ServiceDescriptor(
                    typeof(ITemplyResourceLoader),
                    resourceLoaderType,
                    lifetime));

            return this;
        }

        /// <summary>
        ///     Adds instance of <see cref="ITemplyResourceLoader"/>
        /// </summary>
        /// <param name="resourceLoader">Resource loader instance</param>
        public TemplyProfileBuilder Add([NotNull] ITemplyResourceLoader resourceLoader)
        {
            resourceLoader = resourceLoader ?? throw new ArgumentNullException(nameof(resourceLoader));

            _configuration.Services.AddSingleton(resourceLoader);
            return this;
        }

        private void AssertFullyQualified(string path, string prop)
        {
            if (!Path.IsPathFullyQualified(path))
                throw new ArgumentException("Might be fully qualified", prop);
        }

        private void AssertDirectoryExists(string path, string prop)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("Path not to directory or directory doesn't exist", prop);
        }

        private void AssertFileExists(string path, string prop)
        {
            if (!File.Exists(path))
                throw new ArgumentException("Path not to file or file doesn't exist", prop);
        }
    }
}