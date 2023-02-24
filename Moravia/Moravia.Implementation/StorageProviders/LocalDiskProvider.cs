using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moravia.Domain.Configuration;
using Moravia.Domain.Enums;
using Moravia.Domain.Interfaces;
using System.IO.Abstractions;

namespace Moravia.Implementation.StorageProviders
{

    /// <summary>
    /// Local disk provider.
    /// </summary>
    public class LocalDiskProvider : IStorageProvider
    {
        /// <summary>The logger.</summary>
        private readonly ILogger<LocalDiskProvider> _logger;

        /// <summary>The local disk configuration.</summary>
        private readonly LocalDiskConfiguration _localDiskConfiguration;

        /// <summary>The file system abstraction.</summary>
        private readonly IFileSystem _fileSystem;

        /// <summary>Initializes a new instance of the <see cref="LocalDiskProvider" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="localDiskConfiguration">The local disk configuration.</param>
        /// <param name="fileSystem">The file system.</param>
        public LocalDiskProvider(ILogger<LocalDiskProvider> logger, IOptions<LocalDiskConfiguration> localDiskConfiguration, IFileSystem fileSystem)
        {
            _logger = logger;
            _localDiskConfiguration = localDiskConfiguration.Value;
            _fileSystem= fileSystem;
        }

        /// <summary>Supported type of storage.</summary>
        public StorageType StorageType => StorageType.Local;

        /// <summary>Save content to storage.</summary>
        /// <param name="documentName">Path identifier.</param>
        /// <param name="content">Stream content to store.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Result task.</returns>
        public async Task Save(string documentName, byte[] content, CancellationToken cancellationToken = default)
        {
            try
            {
                var filePath = _fileSystem.Path.Combine(_localDiskConfiguration.Path, documentName);
                await _fileSystem.File.WriteAllBytesAsync(filePath, content, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing {method} in {provider}", nameof(Save), nameof(LocalDiskProvider));
                throw;
            }
        }

        /// <summary>Load content from storage.</summary>
        /// <param name="documentName">Document name.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Loaded stream.</returns>
        public async Task<Stream> Load(string documentName, CancellationToken cancellationToken = default)
        {
            try
            {
                var filePath = _fileSystem.Path.Combine(_localDiskConfiguration.Path, documentName);
                return await Task.FromResult(_fileSystem.File.OpenRead(filePath));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing {method} in {provider}", nameof(Load), nameof(LocalDiskProvider));
                throw;
            }
        }
    }
}
