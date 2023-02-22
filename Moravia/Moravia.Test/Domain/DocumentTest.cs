using AutoFixture;
using Moravia.Domain.BusinessObjects;
using Moravia.Domain.Interfaces;

namespace Moravia.Test.Domain
{
    [TestFixture]
    public class DocumentTest
    {
        [SetUp]
        public void SetUp()
        {
        }


        [Test]
        public void Document_Constructor_Test()
        {

            var documentName = "Name";
            var title = "Title";
            var text = "Text";

            var document = new Document(documentName, title, text);

            Assert.That(document.Title, Is.EqualTo(title));
            Assert.That(document.DocumentName, Is.EqualTo(documentName));
            Assert.That(document.Text, Is.EqualTo(text));
        }

        [Test]
        public void Implement_Interface_Test()
        {
            var fixture = new Fixture();
            IFormatDocument iFormatDocument = fixture.Create<Document>();
            Assert.IsNotNull(iFormatDocument);
        }

        [Test]
        public void Export_Content_Test()
        {

            //// Arrange
            var fixture = new Fixture();
            var document = fixture.Create<Document>();

            var resultContent = document.ExportContent();

            //keeps expected order
            Assert.That(resultContent[0].Key, Is.EqualTo(nameof(document.Title)));
            Assert.That(resultContent[1].Key, Is.EqualTo(nameof(document.Text)));

            //contains expected content
            Assert.That(resultContent[nameof(document.Title)], Is.EqualTo(document.Title));
            Assert.That(resultContent[nameof(document.Text)], Is.EqualTo(document.Text));
        }

        [Test]
        public void Import_Content_Test()
        {
            //// Arrange
            var fixture = new Fixture();
            var sourceDocument = fixture.Create<Document>();
            var content = sourceDocument.ExportContent();

            var destDocument = new Document();
            destDocument.ImportContent(content, sourceDocument.DocumentName);

            Assert.That(destDocument.DocumentName, Is.EqualTo(sourceDocument.DocumentName));
            Assert.That(destDocument.Title, Is.EqualTo(sourceDocument.Title));
            Assert.That(destDocument.Text, Is.EqualTo(sourceDocument.Text));
        }

    }
}
