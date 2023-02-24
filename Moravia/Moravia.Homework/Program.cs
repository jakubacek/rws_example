using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moravia.Domain.BusinessObjects;
using Moravia.Domain.Enums;


namespace Moravia.Homework
{

    internal class Program
    {
        static async Task Main(string[] args)
        {

            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(app => { app.AddJsonFile("appsettings.json"); })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddServiceRegistration(hostContext.Configuration); // format providers and storage providers registration
                }).Build();

            var docName = "documentName";
            await CreateJsonLocal(host.Services, docName);
            await JsonToYamlLocal(host.Services, docName);
            await YamlToXmlLocal(host.Services, docName);

            // Setup connection string in appsettings.json
            //await CreateJsonAzure(host.Services, docName);
            
            Console.WriteLine();
            Console.WriteLine();
            
            await host.RunAsync();
        }


        /// <summary>Creates the json local.</summary>
        /// <param name="hostProvider">The host provider.</param>
        /// <param name="documentName">Document name.</param>
        private static async Task CreateJsonLocal(IServiceProvider hostProvider, string documentName)
        {
            var providerResolver = hostProvider.GetProviderResolver();

            var storageProvider = providerResolver.GetIStorageProvider(StorageType.Local);
            var formatProvider = providerResolver.GetDocumentFormatProvider(FormatType.Json);

            var document = new Document(documentName, "Document title", "Document text."); // create document
            var byteContent = formatProvider.SaveDocumentToFormat(document); // document to json

            var docFullName = $"{documentName}.json";
            await storageProvider.Save(docFullName, byteContent); // save to storage
            Console.WriteLine($"Stored document {docFullName} into c:\\temp");
        }


        /// <summary>Json to yaml local.</summary>
        /// <param name="hostProvider">The host provider.</param>
        /// <param name="documentName">Document name.</param>
        private static async Task JsonToYamlLocal(IServiceProvider hostProvider, string documentName)
        {
            var providerResolver = hostProvider.GetProviderResolver();

            var storageProvider = providerResolver.GetIStorageProvider(StorageType.Local);
            var inputJsonProvider = providerResolver.GetDocumentFormatProvider(FormatType.Json);
            var outputYamlProvider = providerResolver.GetDocumentFormatProvider(FormatType.Yaml);

            var docFullName = $"{documentName}.json";
            await using var memStr =  await storageProvider.Load($"{docFullName}"); // load file from storage
            var document = inputJsonProvider.LoadDocumentFromFormat<Document>(memStr); // from json to document
            Console.WriteLine($"Loaded document {docFullName} from c:\\temp");

            docFullName = $"{documentName}.yaml";
            var byteContent = outputYamlProvider.SaveDocumentToFormat<Document>(document); // from document to yaml
            await storageProvider.Save($"{docFullName}", byteContent); // save to disk
            Console.WriteLine($"Stored document {docFullName} into c:\\temp");
        }


        /// <summary>Yaml to XML local.</summary>
        /// <param name="hostProvider">The host provider.</param>
        /// <param name="documentName">Document name.</param>
        static async Task YamlToXmlLocal(IServiceProvider hostProvider, string documentName)
        {
            var providerResolver = hostProvider.GetProviderResolver();

            var storageProvider = providerResolver.GetIStorageProvider(StorageType.Local);
            var inputYamlProvider = providerResolver.GetDocumentFormatProvider(FormatType.Yaml);
            var outputXmlProvider = providerResolver.GetDocumentFormatProvider(FormatType.Xml);

            var docFullName = $"{documentName}.yaml";
            await using var memStr = await storageProvider.Load($"{docFullName}"); // load file from storage
            var document = inputYamlProvider.LoadDocumentFromFormat<Document>(memStr); // from yaml to document
            Console.WriteLine($"Loaded document {docFullName} from c:\\temp");

            docFullName = $"{documentName}.xml";
            var byteContent = outputXmlProvider.SaveDocumentToFormat<Document>(document); // from document to xml
            await storageProvider.Save($"{docFullName}", byteContent); // save to disk
            Console.WriteLine($"Stored document {docFullName} into c:\\temp");
        }

        /// <summary>Creates the json in azure storage.</summary>
        /// <param name="hostProvider">The host provider.</param>
        /// <param name="documentName">Document name.</param>
        private static async Task CreateJsonAzure(IServiceProvider hostProvider, string documentName)
        {
            var providerResolver = hostProvider.GetProviderResolver();

            var storageProvider = providerResolver.GetIStorageProvider(StorageType.AzureBlob);
            var formatProvider = providerResolver.GetDocumentFormatProvider(FormatType.Json);

            var document = new Document(documentName, "Document title", "Document text."); // create document
            var byteContent = formatProvider.SaveDocumentToFormat(document); // document to json

            var docFullName = $"{documentName}.json";
            await storageProvider.Save(docFullName, byteContent); // save to azure storage
            Console.WriteLine($"Stored document {docFullName} into Azure table storage.");

            var documentFromAzure = await storageProvider.Load(docFullName); // load document from azure storage
            Console.WriteLine($"Loaded document {docFullName} from Azure table storage.");
        }
    }
}