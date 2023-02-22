using Moravia.Domain.Internal;

namespace Moravia.Domain.Interfaces
{
    public interface IFormatDocument
    {
        /// <summary>
        /// Document identifier.
        /// </summary>
        string DocumentName { get; set; }

        /// <summary>
        /// Export document content to key/value ordered pair list.
        /// </summary>
        /// <returns>FormatDocument content.</returns>
        PairList<string, string> ExportContent();

        /// <summary>
        /// Import formatDocument from key/value ordered pair list.
        /// </summary>
        /// <param name="content">Content in key/value ordered pair list.</param>
        /// <param name="documentName">Document name.</param>
        void ImportContent(PairList<string, string> content, string documentName);

    }
}
