using Microsoft.Extensions.Logging;
using Moravia.Domain.Enums;
using Moravia.Domain.Interfaces;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Moravia.Implementation.FormatProviders
{
    /// <summary>
    /// Yaml format provider.
    /// </summary>
    /// <seealso cref="Moravia.Domain.Interfaces.IDocumentFormatProvider" />
    public class YamlFormatProvider : IDocumentFormatProvider
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<YamlFormatProvider> _logger;

        private readonly IDeserializer _deserializer;
        private readonly ISerializer _serializer;

        /// <summary>
        /// Initialize instance of YamlFormatProvider. 
        /// </summary>
        /// <param name="logger">Logger.</param>
        public YamlFormatProvider(ILogger<YamlFormatProvider> logger)
        {
            _logger = logger;
            _deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            _serializer = new YamlDotNet.Serialization.SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        }

        /// <summary>
        /// Type of format supported by provider.
        /// </summary>
        public FormatType FormatType => FormatType.Yaml;

        /// <summary>
        /// Loads the document from format.
        /// </summary>
        /// <typeparam name="T">Type of document.</typeparam>
        /// <param name="inputStream">The input stream.</param>
        /// <returns>Loaded document from stream input.</returns>
        public T LoadDocumentFromFormat<T>(Stream inputStream) where T : class, IFormatDocument
        {
            if (inputStream is { CanRead: true })
            {
                try
                {
                    using var strReader = new StreamReader(inputStream);
                    var resultContent = _deserializer.Deserialize<T>(strReader);
                    return resultContent;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while executing {method} in {provider}", nameof(LoadDocumentFromFormat), nameof(YamlFormatProvider));
                }
            }
            return null;
        }

        /// <summary>
        /// Saves the document to format.
        /// </summary>
        /// <typeparam name="T">Type of document.</typeparam>
        /// <param name="documentContent">Content of the document.</param>
        /// <returns>UTF-8 encoded text in result format in byte array.</returns>
        public byte[] SaveDocumentToFormat<T>(T documentContent) where T : class, IFormatDocument
        {
            try
            {
                var stringResult = _serializer.Serialize(documentContent);
                return Encoding.UTF8.GetBytes(stringResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing {method} in {provider}.", nameof(SaveDocumentToFormat), nameof(YamlFormatProvider));
            }

            return new byte[] { };
        }
    
    }
}
