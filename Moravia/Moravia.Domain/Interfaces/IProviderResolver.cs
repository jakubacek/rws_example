using Moravia.Domain.Enums;

namespace Moravia.Domain.Interfaces
{
    /// <summary>
    /// Providers resolver.
    /// </summary>
    public interface IProviderResolver
    {
        /// <summary>
        /// Gets the document format provider.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Format provider of requested type.</returns>
        IDocumentFormatProvider GetDocumentFormatProvider(FormatType type);

        /// <summary>
        /// Gets the i storage provider.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Storage provider of requested type.</returns>
        IStorageProvider GetIStorageProvider(StorageType type);
    }
}
