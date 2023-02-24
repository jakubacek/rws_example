using Microsoft.Extensions.Logging;
using Moravia.Domain.Enums;
using Moravia.Domain.Interfaces;
using System.Text;
using System.Xml.Serialization;

namespace Moravia.Implementation.FormatProviders
{
    /// <summary>
    /// Xml format provider.
    /// </summary>
    /// <seealso cref="Moravia.Domain.Interfaces.IDocumentFormatProvider" />
    public class XmlFormatProvider : IDocumentFormatProvider
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<XmlFormatProvider> _logger;

        /// <summary>
        /// Initialize instance of XmlFormatProvider. 
        /// </summary>
        /// <param name="logger">Logger.</param>
        public XmlFormatProvider(ILogger<XmlFormatProvider> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Type of format supported by provider.
        /// </summary>
        public FormatType FormatType => FormatType.Xml;

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
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    var result = xmlSerializer.Deserialize(inputStream) as T;
                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while executing {method} in {provider}", nameof(LoadDocumentFromFormat), nameof(XmlFormatProvider));
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
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                
                using StringWriter textWriter = new StringWriter();
                xmlSerializer.Serialize(textWriter, documentContent);
                var stringResult = textWriter.ToString();
                return Encoding.Unicode.GetBytes(stringResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing {method} in {provider}.", nameof(SaveDocumentToFormat), nameof(XmlFormatProvider));
            }

            return new byte[] { };
        }
    }
}
