using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moravia.Domain.Configuration;
using Moravia.Domain.Enums;
using Moravia.Domain.Interfaces;

namespace Moravia.Implementation.StorageProviders
{
    /// <summary>
    /// Azure blob providers.
    /// </summary>
    public class AzureBlobProvider : IStorageProvider
    {
        /// <summary>Initializes a new instance of the <see cref="AzureBlobProvider" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationOptions">The configuration option.</param>
        public AzureBlobProvider(ILogger<AzureBlobProvider> logger, IOptions<AzureBlobStorageConfiguration> configurationOptions)
        {
            _logger = logger;
            var configuration = configurationOptions.Value;
            _blobContainerClient = new BlobContainerClient(configuration.ConnectionString, configuration.Container?.ToLower());
        }

        /// <summary>The logger.</summary>
        private readonly ILogger<AzureBlobProvider> _logger;

        /// <summary>The BLOB container client</summary>
        private readonly BlobContainerClient _blobContainerClient;

        /// <summary>Supported type of storage.</summary>
        public StorageType StorageType => StorageType.AzureBlob;

        /// <summary>Save content to storage.</summary>
        /// <param name="documentName">Document name.</param>
        /// <param name="content">Stream content to store.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Save(string documentName, byte[] content, CancellationToken cancellationToken = default)
        {
            try
            {
                await _blobContainerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.None, null, null, cancellationToken);
                var blobClient = _blobContainerClient.GetBlobClient(documentName);
                await blobClient.UploadAsync(BinaryData.FromBytes(content),true, cancellationToken );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing {method} in {provider}", nameof(Save), nameof(AzureBlobProvider));
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
                await _blobContainerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.None, null, null, cancellationToken);
                var blobClient = _blobContainerClient.GetBlobClient(documentName);
                var memoryStream = new MemoryStream();
                await blobClient.DownloadToAsync(memoryStream, cancellationToken);
                return memoryStream;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing {method} in {provider}", nameof(Load), nameof(AzureBlobProvider));
                throw;
            }
        }
    }
}
