using Microsoft.Extensions.Logging;
using Moravia.Domain.Enums;
using Moravia.Domain.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Moravia.Implementation.FormatProviders
{

    /// <summary>
    /// Json format provider.
    /// </summary>
    /// <seealso cref="Moravia.Domain.Interfaces.IDocumentFormatProvider" />
    public class JsonFormatProvider : IDocumentFormatProvider
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<JsonFormatProvider> _logger;

        /// <summary>
        /// Initialize instance of JsonFormatProvider. 
        /// </summary>
        /// <param name="logger">Logger.</param>
        public JsonFormatProvider(ILogger<JsonFormatProvider> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Type of format supported by provider.
        /// </summary>
        public FormatType FormatType => FormatType.Json;

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
                    var content = strReader.ReadToEnd();

                    var resultContent = JsonConvert.DeserializeObject<T>(content);
                    return resultContent;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while executing {method} in {provider}.", nameof(LoadDocumentFromFormat), nameof(JsonFormatProvider));
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
                var stringResult = JsonConvert.SerializeObject(documentContent);
                var byteResult = Encoding.UTF8.GetBytes(stringResult);
                return byteResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing {method} in {provider}.", nameof(SaveDocumentToFormat), nameof(JsonFormatProvider));
            }

            return new byte[] { };
        }
    }
}
