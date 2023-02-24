using Moravia.Domain.Enums;

namespace Moravia.Domain.Interfaces
{
    /// <summary>
    /// Storage provider interface.
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// Supported type of storage.
        /// </summary>
        StorageType StorageType { get; }

        /// <summary>
        /// Save content to storage.
        /// </summary>
        /// <param name="documentName">Document name.</param>
        /// <param name="content">Stream content to store.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Result task.</returns>
        Task Save(string documentName, byte[] content, CancellationToken cancellationToken = default);

        /// <summary>
        /// Load content from storage.
        /// </summary>
        /// <param name="documentName">Document name.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Loaded stream.</returns>
        Task<Stream> Load(string documentName, CancellationToken cancellationToken = default);
    }
}
