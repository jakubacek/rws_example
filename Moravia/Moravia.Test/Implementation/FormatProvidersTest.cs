using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using Moravia.Domain.BusinessObjects;
using Moravia.Domain.Enums;
using Moravia.Domain.Interfaces;
using Moravia.Implementation.FormatProviders;

namespace Moravia.Test.Implementation
{
    [TestFixture]
    public class FormatProvidersTest
    {

        /// <summary>Tests the format providers format.</summary>
        /// <param name="formatType">Type of the format.</param>
        [Test]
        [TestCase(FormatType.Json)]
        [TestCase(FormatType.Xml)]
        [TestCase(FormatType.Yaml)]
        public void Test_FormatProviders_Format(FormatType formatType)
        {
            var provider = GetProvider(formatType);
            Assert.That(provider.FormatType, Is.EqualTo(formatType)); // check provider have defined type.
        }

        /// <summary>Check that providers are able to format document to specified format and then load document from format.</summary>
        /// <param name="formatType">Type of the format.</param>
        [Test]
        [TestCase(FormatType.Json)]
        [TestCase(FormatType.Xml)]
        [TestCase(FormatType.Yaml)]
        public void Test_SaveDocument_LoadDocument(FormatType formatType)
        {

            var fixture = new Fixture();
            var inputDocument = fixture.Create<Document>();

            IDocumentFormatProvider set = GetProvider(formatType);
            var documentContent = set.SaveDocumentToFormat(inputDocument); // save input document to bytes in specified format.

            using var memStream = new MemoryStream(documentContent);
            var resultDocument = set.LoadDocumentFromFormat<Document>(memStream); // load result document from stream.

            Assert.That(inputDocument.Title, Is.EqualTo(resultDocument.Title)); // check input == result
            Assert.That(inputDocument.Text, Is.EqualTo(resultDocument.Text));
            Assert.IsNull(resultDocument.DocumentName); // check document name not serialized
        }


        private IDocumentFormatProvider GetProvider(FormatType formatType)
        {
            IDocumentFormatProvider formatProvider = null;
            switch (formatType)
            {
                case FormatType.Json:
                    var loggerJson = new Mock<ILogger<JsonFormatProvider>>().Object;
                    formatProvider = new JsonFormatProvider(loggerJson);
                    break;
                case FormatType.Xml:
                    var loggerXml = new Mock<ILogger<XmlFormatProvider>>().Object;
                    formatProvider = new XmlFormatProvider(loggerXml);
                    break;
                case FormatType.Yaml:
                    var loggerYaml = new Mock<ILogger<YamlFormatProvider>>().Object;
                    formatProvider = new YamlFormatProvider(loggerYaml);
                    break;
            }

            return formatProvider;

        }
    }
}
