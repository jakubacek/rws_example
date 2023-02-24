using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moravia.Domain.Enums;
using Moravia.Domain.Interfaces;
using Moravia.Implementation.FormatProviders;
using Moravia.Implementation.StorageProviders;
using System;

namespace Moravia.Implementation
{

    /// <summary>
    /// Factory for providers resolve.
    /// </summary>
    public class ProviderResolver : IProviderResolver
    {
        private readonly string _notFoundProvider = "Requested provider {0} not found.";

        /// <summary>The service provider</summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>The logger</summary>
        private readonly ILogger<ProviderResolver> _logger;

        /// <summary>Initializes a new instance of the <see cref="ProviderResolver" /> class.</summary>
        /// <param name="logger">Logger.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public ProviderResolver(ILogger<ProviderResolver> logger, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger;
        }

        /// <summary>Gets the document format provider.</summary>
        /// <param name="type">The type.</param>
        /// <returns>Format provider of requested type.</returns>
        public IDocumentFormatProvider GetDocumentFormatProvider(FormatType type)
        {
            var formatProvider = type switch
            {
                FormatType.Json => _serviceProvider.GetService<JsonFormatProvider>() as IDocumentFormatProvider,
                FormatType.Xml => _serviceProvider.GetService<XmlFormatProvider>(),
                FormatType.Yaml => _serviceProvider.GetService<YamlFormatProvider>(),
                _ => null
            };

            if (formatProvider == null)
            {
                var message = string.Format(_notFoundProvider, nameof(type));
                _logger.LogCritical(string.Format(_notFoundProvider, message));
                throw new ArgumentOutOfRangeException(nameof(type), type, message);
            }
            return formatProvider;
        }

        /// <summary>Gets the i storage provider.</summary>
        /// <param name="type">The type.</param>
        /// <returns>Storage provider of requested type.</returns>
        public IStorageProvider GetIStorageProvider(StorageType type)
        {
            var storageProvider = type switch
            {
                StorageType.Local => _serviceProvider.GetService<LocalDiskProvider>() as IStorageProvider,
                StorageType.AzureBlob => _serviceProvider.GetService<AzureBlobProvider>(),
                _ => null
            };

            if (storageProvider == null)
            {
                var message = string.Format(_notFoundProvider, nameof(type));
                _logger.LogCritical(string.Format(_notFoundProvider, message));
                throw new ArgumentOutOfRangeException(nameof(type), type, message);
            }
            return storageProvider;
        }

    }
}
