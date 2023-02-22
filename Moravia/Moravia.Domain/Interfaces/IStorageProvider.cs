using Moravia.Domain.Constants;

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
        /// Save stream content to storage.
        /// </summary>
        /// <param name="documentName">Path identifier.</param>
        /// <param name="content">Stream content to store.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Result task.</returns>
        Task Save(string documentName, byte[] content, CancellationToken cancellationToken = default);

        /// <summary>
        /// Load content from storage.
        /// </summary>
        /// <param name="documentName">Storage path identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Loaded stream.</returns>
        Stream  Load(string documentName, CancellationToken cancellationToken = default);
    }
}
