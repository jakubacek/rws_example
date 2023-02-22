using Microsoft.Extensions.Logging;
using Moravia.Domain.Constants;
using Moravia.Domain.Interfaces;
using Moravia.Domain.Internal;
using System.Xml.Linq;

namespace Moravia.Implementation.FormatProviders
{
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


        public async Task<PairList<string, string>> LoadDocumentFromFormat(Stream inputStream, CancellationToken cancellationToken = default)
        {
            if (inputStream is { CanRead: true })
            {
                try
                {

                    var result = new PairList<string, string>();

                    using var strReader = new StreamReader(inputStream);
                    var xDoc = await XDocument.LoadAsync(inputStream, LoadOptions.None, cancellationToken).ConfigureAwait(false);
                    foreach (var element in xDoc.Descendants())
                    {
                        result.Add(element.Name.LocalName, element.Value);
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while executing {method} in {provider}", nameof(LoadDocumentFromFormat), nameof(XmlFormatProvider));
                }
            }
            return new PairList<string, string>();
        }

        public byte[] SaveDocumentToFormat(PairList<string, string> documentContent)
        {
            throw new NotImplementedException();
        }
    }
}
