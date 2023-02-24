using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moravia.Domain.Configuration;
using Moravia.Domain.Interfaces;
using Moravia.Implementation;
using Moravia.Implementation.FormatProviders;
using Moravia.Implementation.StorageProviders;
using System.Configuration;
using System.IO.Abstractions;

namespace Moravia.Homework
{
    /// <summary>
    ///   <br />
    /// </summary>
    public static class ServicesRegistrationExtension
    {

        /// <summary>Adds the service registration.</summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration"></param>
        /// <returns>Service pipeline after all dependencies registration.</returns>
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IProviderResolver, ProviderResolver>();

            // format providers
            services.AddSingleton<JsonFormatProvider>();
            services.AddSingleton<XmlFormatProvider>();
            services.AddSingleton<YamlFormatProvider>();

            // storage providers
            services.AddSingleton<LocalDiskProvider>();
            services.AddSingleton<AzureBlobProvider>();

            services.AddSingleton<IFileSystem, FileSystem>();

            services.Configure<LocalDiskConfiguration>(configuration.GetSection(LocalDiskConfiguration.LocalDiskConfigurationOption)); // configuration for local disk provider
            services.Configure<AzureBlobStorageConfiguration>(configuration.GetSection(AzureBlobStorageConfiguration.AzureBlobConfigurationOption)); // configuration for azure blob storage
            return services;
        }

        public static IProviderResolver GetProviderResolver(this IServiceProvider hostProvider)
        {
            IServiceScope serviceScope = hostProvider.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            return provider.GetRequiredService<IProviderResolver>();
        }
    }
}
