using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moravia.Domain.Configuration;
using Moravia.Domain.Constants;
using Moravia.Domain.Interfaces;
using Moravia.Implementation.FormatProviders;
using System.IO.Abstractions;

namespace Moravia.Implementation.StorageProviders
{

    /// <summary>
    /// Local disk provider.
    /// </summary>
    public class LocalDiskProvider : IStorageProvider
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<LocalDiskProvider> _logger;

        private readonly LocalDiskConfiguration _localDiskConfiguration;

        private readonly IFileSystem _fileSystem;

        public LocalDiskProvider(ILogger<LocalDiskProvider> logger, IOptions<LocalDiskConfiguration> localDiskConfiguration, IFileSystem fileSystem)
        {
            _logger = logger;
            _localDiskConfiguration = localDiskConfiguration.Value;
            _fileSystem= fileSystem;
        }

        public StorageType StorageType => StorageType.Local;


        public async Task Save(string documentName, byte[] content, CancellationToken cancellationToken = default)
        {
            //var filePath = Path.Combine(_localDiskConfiguration.Path, documentName);
            
        }

        public Stream Load(string documentName, CancellationToken cancellationToken = default)
        {
            try
            {
                var filePath = _fileSystem.Path.Combine(_localDiskConfiguration.Path, documentName);
                return _fileSystem.File.OpenRead(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing {method} in {provider}", nameof(Load), nameof(LocalDiskProvider));
                throw;
            }
        }
    }
}
