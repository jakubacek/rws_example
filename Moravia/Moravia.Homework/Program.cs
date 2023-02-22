using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moravia.Domain.Interfaces;
using Moravia.Implementation;
using Moravia.Implementation.FormatProviders;
using Moravia.Implementation.StorageProviders;

namespace Moravia.Homework
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                
            }

            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {                    
                    // resolver factory
                    services.AddSingleton<IProviderResolver, ProviderResolver>();

                    // format providers
                    services.AddTransient<JsonFormatProvider>();
                    services.AddTransient<XmlFormatProvider>();
                    services.AddTransient<YamlFormatProvider>();

                    // storage providers
                    services.AddTransient<LocalDiskProvider>();

                })
                .RunConsoleAsync();

            //CreateHostBuilder(args).Build().Run();

         
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureServices((hostContext, services) =>
        //        {
        //            // resolver factory
        //            services.AddSingleton<IProviderResolver, ProviderResolver>();

        //            // format providers
        //            services.AddTransient<JsonFormatProvider>();
        //            services.AddTransient<XmlFormatProvider>();
        //            services.AddTransient<YamlFormatProvider>();

        //            // storage providers
        //            services.AddTransient<LocalDiskProvider>();

        //            //services.AddScoped<IFileSystem, FileSystem>();


        //        });
    }
}