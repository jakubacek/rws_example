using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using System.Text.Unicode;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moravia.Domain.Configuration;
using Moravia.Implementation.StorageProviders;

namespace Moravia.Test.Implementation
{
    [TestFixture]
    public class LocalDiskProviderTest
    {
        /// <summary>The logger</summary>
        private ILogger<LocalDiskProvider> _logger;

        /// <summary>The configuration</summary>
        private IOptions<LocalDiskConfiguration> _configuration;

        /// <summary>The file system abstraction</summary>
        private IFileSystem _fileSystem;

        private readonly string _fileName = "test.txt";
        private readonly string _fileContent = "Test content.";
        private readonly string _folderPath = "c:\\temp";

        [SetUp]
        public void SetUp()
        {

            var testingFile = Path.Combine(_folderPath, _fileName);

            _logger = new Mock<ILogger<LocalDiskProvider>>().Object;
            _configuration = Options.Create(new LocalDiskConfiguration { Path = _folderPath });
            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {testingFile, _fileContent}
            });
        }

        [Test]
        public async Task Load_Test_Existing_File()
        {
            var diskProvider = new LocalDiskProvider(_logger, _configuration, _fileSystem);
            using var stream = await diskProvider.Load(_fileName);
            using var sr = new StreamReader(stream);
            var result = await sr.ReadToEndAsync();

            Assert.That(result, Is.EqualTo(_fileContent));
        }

        [Test]
        public void Load_Test_Not_Existing_File()
        {
            var diskProvider = new LocalDiskProvider(_logger, _configuration, _fileSystem);
            Assert.ThrowsAsync<FileNotFoundException>(async () => await diskProvider.Load("notExisting.txt"));
        }

        [Test]
        public async Task Save_Test()
        {
            var fileNameToSave = "newFile.txt";
            var diskProvider = new LocalDiskProvider(_logger, _configuration, _fileSystem);
            var bytes = Encoding.UTF8.GetBytes(_fileContent);
            await diskProvider.Save(fileNameToSave, bytes);
            var resultStream = await diskProvider.Load(fileNameToSave);
            Assert.IsNotNull(resultStream);
        }
    }
}
