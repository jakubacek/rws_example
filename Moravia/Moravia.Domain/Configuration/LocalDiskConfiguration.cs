namespace Moravia.Domain.Configuration
{
    /// <summary>
    /// Local disk storage provider configuration
    /// </summary>
    public class LocalDiskConfiguration
    {
        /// <summary>The local disk configuration option</summary>
        public const string LocalDiskConfigurationOption = "LocalDisk";

        /// <summary>
        /// Gets or sets the path.
        /// </summary
        public string Path { get; set; }
    }
}
