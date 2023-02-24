using Moravia.Domain.Enums;

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

        /// <summary>
        /// Loads the document from format.
        /// </summary>
        /// <typeparam name="T">Type of document.</typeparam>
        /// <param name="inputStream">The input stream.</param>
        /// <returns>Loaded document from stream input.</returns>
        T LoadDocumentFromFormat<T>(Stream inputStream) where T : class, IFormatDocument;

        /// <summary>
        /// Saves the document to format.
        /// </summary>
        /// <typeparam name="T">Type of document.</typeparam>
        /// <param name="documentContent">Content of the document.</param>
        /// <returns>UTF-8 encoded text in result format in byte array.</returns>
        byte[] SaveDocumentToFormat<T>(T documentContent) where T : class, IFormatDocument;
    }
}
