namespace Moravia.Domain.Interfaces
{
    interface IDocument
    {
        /// <summary>
        /// Document identifier.
        /// </summary>
        string DocumentName { get; set; }

        /// <summary>
        /// Export document content to key/value dictionary.
        /// </summary>
        /// <returns>Document content.</returns>
        IDictionary<string, string> ExportContent();

        /// <summary>
        /// Import document from key/value dictionary.
        /// </summary>
        /// <param name="content">Content in key/value dictionary.</param>
        /// <param name="documentName">Document name.</param>
        void ImportContent(IDictionary<string, string> content, string documentName);

    }
}
