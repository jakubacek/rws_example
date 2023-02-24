using Moravia.Domain.Interfaces;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using YamlDotNet.Serialization;

namespace Moravia.Domain.BusinessObjects
{
    /// <summary>
    /// Document
    /// </summary>
    [Serializable]
    public class Document : IFormatDocument
    {
        /// <summary>
        /// Initialize new instance of Document.
        /// </summary>
        /// <param name="documentName">Document name.</param>
        /// <param name="title">Title of document.</param>
        /// <param name="text">Text of document.</param>
        public Document(string documentName, string title, string text)
        {
            DocumentName = documentName;
            Title = title;
            Text = text;
        }

        public Document() { }

        /// <summary>
        /// FormatDocument title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// FormatDocument text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// FormatDocument name.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        [IgnoreDataMember]
        [YamlIgnore]
        public string DocumentName { get; set; }


        /// <summary>
        /// To String implementation.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return $"{DocumentName} {Title} {Text}";
        }

        /// <summary>
        /// Get HashCode implementation.
        /// </summary>
        /// <returns>Hash Code.</returns>
        public override int GetHashCode()
        {
            var name = DocumentName ?? string.Empty;
            var title = Title ?? string.Empty;
            var text = Text ?? string.Empty;

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
