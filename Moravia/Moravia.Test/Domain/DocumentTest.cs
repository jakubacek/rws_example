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
        public void Implement_ToString_Test()
        {
            var fixture = new Fixture();
            var document = fixture.Create<Document>();
            Assert.IsNotNull(document.ToString());
            Assert.IsTrue(document.ToString().Contains(document.Title));
            Assert.IsTrue(document.ToString().Contains(document.DocumentName));
            Assert.IsTrue(document.ToString().Contains(document.Text));
        }

        [Test]
        public void Implement_Equal_Test()
        {

            var documentA = new Document("a", "b", "c");
            var documentB = new Document("a", "b", "c");

            Assert.That(documentA, Is.EqualTo(documentB));
            Assert.IsTrue(documentA.Equals(documentB));
        }

        [Test]
        public void Implement_Interface_Test()
        {
            var fixture = new Fixture();
            IFormatDocument iFormatDocument = fixture.Create<Document>();
            Assert.IsNotNull(iFormatDocument);
        }
    }
}
