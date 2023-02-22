using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moravia.Domain.Configuration;
using Moravia.Domain.Internal;
using Moravia.Implementation;
using Moravia.Implementation.StorageProviders;

namespace Moravia.Test.Implementation
{
    [TestFixture]
    public class LocalDiskProviderTest
    {
        private ILogger<LocalDiskProvider> _logger;
        private IOptions<LocalDiskConfiguration> _configuration;
        private IFileSystem _fileSystem;

        private readonly string _fileName = "test.txt";
        private readonly string _fileContent = "Test content.";


        [SetUp]
        public void SetUp()
        {
            var folderPath = "c:\\temp";
            var testingFile = Path.Combine(folderPath, _fileName);

            _logger = new Mock<ILogger<LocalDiskProvider>>().Object;
            _configuration = Options.Create(new LocalDiskConfiguration { Path = folderPath });
            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {testingFile, _fileContent}
            });
        }

        [Test]
        public void Load_Test_Existing_File()
        {
            var diskProvider = new LocalDiskProvider(_logger, _configuration, _fileSystem);
            using var stream = diskProvider.Load(_fileName);
            using var sr = new StreamReader(stream);
            var result = sr.ReadToEnd();

            Assert.That(result, Is.EqualTo(_fileContent));
        }

        [Test]
        public void Load_Test_Not_Existing_File()
        {
            var diskProvider = new LocalDiskProvider(_logger, _configuration, _fileSystem);
            Assert.Throws<FileNotFoundException>(() => diskProvider.Load("notExisting.txt"));
        }
    }
}
