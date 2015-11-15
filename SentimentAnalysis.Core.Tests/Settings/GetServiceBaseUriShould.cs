using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.Core.Tests.Settings
{
    using System;
    using System.Configuration;

    using AssertExLib;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Settings = Core.Settings;

    [TestClass]
    public class GetServiceBaseUriShould : SettingsTest
    {
        [TestMethod]
        public void ReturnDefaultWhenProvided()
        {
            const string expected = "https://www.example.com";

            Sut = new Settings(new Uri(expected));

            var result = this.Sut.GetServiceBaseUri();

            Assert.AreEqual(new Uri(expected), result);
        }

        [TestMethod]
        public void ThrowUriFormatExceptionWithCustomMessageWhenInvalidInConfiguration()
        {
            ConfigurationManager.AppSettings[Constants.ServiceBaseUriConfigKey] = "foo";

            AssertEx.Throws<UriFormatException>(() => this.Sut.GetServiceBaseUri());
        }

        [TestMethod]
        public void ReturnUriFromConfiguration()
        {
            var expected = new Uri(@"https://www.example.com");
            ConfigurationManager.AppSettings[Constants.ServiceBaseUriConfigKey] = expected.ToString();

            var result = this.Sut.GetServiceBaseUri();
            Assert.AreEqual(expected, result);
        }
    }
}
