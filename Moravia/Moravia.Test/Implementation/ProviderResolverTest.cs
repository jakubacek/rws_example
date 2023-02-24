using Microsoft.Extensions.Logging;
using Moq;
using Moravia.Domain.Enums;
using Moravia.Implementation;
using Moravia.Implementation.FormatProviders;

namespace Moravia.Test.Implementation
{

    [TestFixture]
    public class ProviderResolverTest
    {
        ProviderResolver _resolver;


        [SetUp]
        public void SetUp()
        {

            var jsonFormatProviderLogger = new Mock<ILogger<JsonFormatProvider>>().Object;
            var xmlFormatProviderLogger = new Mock<ILogger<XmlFormatProvider>>().Object;
            var yamlFormatProviderLogger = new Mock<ILogger<YamlFormatProvider>>().Object;

            var logger = new Mock<ILogger<ProviderResolver>>().Object;

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(JsonFormatProvider)))    
                .Returns(new JsonFormatProvider(jsonFormatProviderLogger));

            serviceProviderMock.Setup(x => x.GetService(typeof(XmlFormatProvider)))
                .Returns(new XmlFormatProvider(xmlFormatProviderLogger));

            serviceProviderMock.Setup(x => x.GetService(typeof(YamlFormatProvider)))
                .Returns(new YamlFormatProvider(yamlFormatProviderLogger));

            _resolver =  new ProviderResolver(logger, serviceProviderMock.Object);
        }


        /// <summary>Tests the format providers format.</summary>
        [Test]
        public void Test_FormatProviders_Format()
        {
            foreach (var formatType in Enum.GetValues(typeof(FormatType)))
            {
                var formatResolver = _resolver.GetDocumentFormatProvider((FormatType)formatType);
                Assert.IsNotNull(formatResolver);
            }
        }
    }
}
