using Microsoft.Extensions.DependencyInjection;
using Moravia.Domain.Constants;
using Moravia.Domain.Interfaces;
using Moravia.Implementation.FormatProviders;
using Moravia.Implementation.StorageProviders;

namespace Moravia.Implementation
{

    /// <summary>
    /// Factory for providers resolve.
    /// </summary>
    public class ProviderResolver : IProviderResolver
    {
        private readonly string _notFoundProvider = "Requested provider {0} not found.";

        private readonly IServiceProvider _serviceProvider;

        /// <summary>Initializes a new instance of the <see cref="ProviderResolver" /> class.</summary>
        /// <param name="serviceProvider">The service provider.</param>
        public ProviderResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>Gets the document format provider.</summary>
        /// <param name="type">The type.</param>
        /// <returns>Format provider of requested type.</returns>
        public IDocumentFormatProvider GetDocumentFormatProvider(FormatType type)
        {
            return type switch
            {
                FormatType.Json => _serviceProvider.GetService<JsonFormatProvider>() as IDocumentFormatProvider,
                FormatType.Xml => _serviceProvider.GetService<XmlFormatProvider>(),
                FormatType.Yaml => _serviceProvider.GetService<YamlFormatProvider>(),
                _ => null
            } ?? throw new ArgumentOutOfRangeException(nameof(type), type, string.Format(_notFoundProvider, nameof(type)));
        }

        /// <summary>Gets the i storage provider.</summary>
        /// <param name="type">The type.</param>
        /// <returns>Storage provider of requested type.</returns>
        public IStorageProvider GetIStorageProvider(StorageType type)
        {
            return type switch
            {
                StorageType.Local => _serviceProvider.GetService<LocalDiskProvider>() as IStorageProvider,
                _ => null
            } ?? throw new ArgumentOutOfRangeException(nameof(type), type, string.Format(_notFoundProvider, nameof(type)));
        }
    }
}
