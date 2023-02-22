using Microsoft.Extensions.Logging;
using Moravia.Domain.Constants;
using Moravia.Domain.Interfaces;
using Moravia.Domain.Internal;
using Newtonsoft.Json;

namespace Moravia.Implementation.FormatProviders
{

    /// <summary>
    /// Json format provider.
    /// </summary>
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


        public async Task<PairList<string, string>> LoadDocumentFromFormat(Stream inputStream, CancellationToken cancellationToken = default)
        {
            if (inputStream is { CanRead: true })
            {
                try
                {
                    using var strReader = new StreamReader(inputStream);
                    var content = await strReader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);
                    
                    var resultContent = JsonConvert.DeserializeObject(content) as PairList<string, string>;
                    return resultContent ?? new PairList<string, string>();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while executing {method} in {provider}", nameof(LoadDocumentFromFormat), nameof(JsonFormatProvider));
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
