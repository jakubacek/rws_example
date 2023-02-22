using Moravia.Domain.Constants;
using Moravia.Domain.Internal;

namespace Moravia.Domain.Interfaces
{
    /// <summary>
    /// Format provider interface.
    /// </summary>
    public interface IDocumentFormatProvider
    {

        /// <summary>
        /// Supported type of format.
        /// </summary>
        FormatType FormatType { get; }

        Task<PairList<string, string>> LoadDocumentFromFormat(Stream inputStream, CancellationToken cancellationToken = default);

        byte[] SaveDocumentToFormat(PairList<string, string> documentContent);
    }
}
