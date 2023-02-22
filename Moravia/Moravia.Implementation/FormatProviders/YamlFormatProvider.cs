using Microsoft.Extensions.Logging;
using Moravia.Domain.Constants;
using Moravia.Domain.Interfaces;
using Moravia.Domain.Internal;
using YamlDotNet.Serialization.NamingConventions;

namespace Moravia.Implementation.FormatProviders
{
    /// <summary>
    /// Yaml format provider.
    /// </summary>
    public class YamlFormatProvider : IDocumentFormatProvider
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<YamlFormatProvider> _logger;

        /// <summary>
        /// Initialize instance of YamlFormatProvider. 
        /// </summary>
        /// <param name="logger">Logger.</param>
        public YamlFormatProvider(ILogger<YamlFormatProvider> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Type of format supported by provider.
        /// </summary>
        public FormatType FormatType => FormatType.Yaml;


        public async Task<PairList<string, string>> LoadDocumentFromFormat(Stream inputStream, CancellationToken cancellationToken = default)
        {
            if (inputStream is { CanRead: true })
            {
                try
                {
                    using var strReader = new StreamReader(inputStream);
                    var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
                        .WithNamingConvention(CamelCaseNamingConvention.Instance)
                        .Build();
                    var resultContent = deserializer.Deserialize(strReader) as PairList<string, string>;
                    return await Task.FromResult(resultContent ?? new PairList<string, string>());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while executing {method} in {provider}", nameof(LoadDocumentFromFormat), nameof(YamlFormatProvider));
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
