namespace SentimentAnalysis.AzureMachineLearning.Tests.Settings
{
    using System;
    using System.Configuration;
    using AssertExLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetServiceBaseUriShould : SettingsTest
    {
        [TestMethod]
        public void ReturnDefaultWhenNotConfigured()
        {
            var expected = new Uri(Constants.DefaultServiceBaseUri);

            Assert.IsNull(ConfigurationManager.AppSettings[Constants.ServiceBaseUriConfigKey]);

            var result = Sut.GetServiceBaseUri();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ThrowUriFormatExceptionWithCustomMessageWhenInvalidInConfiguration()
        {
            ConfigurationManager.AppSettings[Constants.ServiceBaseUriConfigKey] = "foo";

            AssertEx.Throws<UriFormatException>(() => Sut.GetServiceBaseUri());
        }

        [TestMethod]
        public void ReturnUriFromConfiguration()
        {
            var expected = new Uri(@"https://www.example.com");
            ConfigurationManager.AppSettings[Constants.ServiceBaseUriConfigKey] = expected.ToString();

            var result = Sut.GetServiceBaseUri();
            Assert.AreEqual(expected, result);
        }
    }
}