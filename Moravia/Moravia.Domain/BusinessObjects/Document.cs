using Moravia.Domain.Interfaces;

namespace Moravia.Domain.BusinessObjects
{
    /// <summary>
    /// Document
    /// </summary>
    public class Document : IDocument
    {
        /// <summary>
        /// Initialize new instance of Document.
        /// </summary>
        /// <param name="title">Title of document.</param>
        /// <param name="text">Text of document.</param>
        /// <param name="name">Document name.</param>
        public Document(string title, string text, string name)
        {
            Title = title;
            Text = text;
            DocumentName = name;
        }

        /// <summary>
        /// Initialize new instance of Document.
        /// </summary>
        public Document()
        {

        }

        /// <summary>
        /// Document title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Document text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Document name.
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// Export document content to key/value dictionary.
        /// </summary>
        /// <returns>Document content.</returns>
        public IDictionary<string, string> ExportContent()
        {
            var result = new Dictionary<string, string>
            {
                { nameof(Title), Title },
                { nameof(Text), Text }
            };
            return result;
        }

        /// <summary>
        /// Import document from key/value dictionary.
        /// </summary>
        /// <param name="content">Content in key/value dictionary.</param>
        /// <param name="documentName">Document name.</param>
        public void ImportContent(IDictionary<string, string> content, string documentName)
        {
            DocumentName = documentName;
            Title = content?[nameof(Title)] ?? string.Empty;
            Text = content?[nameof(Text)] ?? string.Empty;
        }

        /// <summary>
        /// To String implementation.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return $"{Title} {Text}";
        }

        /// <summary>
        /// Get HashCode implementation.
        /// </summary>
        /// <returns>Hash Code.</returns>
        public override int GetHashCode()
        {
            var title = Title ?? string.Empty;
            var text = Text ?? string.Empty;
            var name = DocumentName ?? string.Empty;
            return title.GetHashCode() * 17 + text.GetHashCode() + name.GetHashCode();
        }

        /// <summary>
        /// Equal implementation.
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>True if both object are equal.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is Document document)
            {
                return this.Title == document.Title && this.Text == document.Text && this.DocumentName == document.DocumentName;
            }
            return false;
        }

    }
}
