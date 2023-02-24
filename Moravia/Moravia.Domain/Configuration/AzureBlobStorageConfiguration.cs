namespace Moravia.Domain.Configuration
{
    /// <summary>
    /// Azure blob storage configuration.
    /// </summary>
    public class AzureBlobStorageConfiguration
    {
        public const string AzureBlobConfigurationOption = "AzureBlob";

        /// <summary>
        /// Connection string.
        /// TODO move to key vault.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>Container name.</summary>
        public string Container { get; set; }
    }
}
