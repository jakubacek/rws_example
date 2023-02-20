namespace Moravia.Domain.Interfaces
{
    /// <summary>
    /// Storage provider interface.
    /// </summary>
    interface IStorageProvider
    {
        /// <summary>
        /// Save stream content to storage.
        /// </summary>
        /// <param name="pathIdentifier">Path identifier.</param>
        /// <param name="content">Stream content to store.</param>
        /// <returns>Result task.</returns>
        Task Save(string pathIdentifier, Stream content);

        /// <summary>
        /// Load content from storage.
        /// </summary>
        /// <param name="pathIdentifier">Storage path identifier.</param>
        /// <returns>Loaded stream.</returns>
        Stream Load(string pathIdentifier);
    }
}
