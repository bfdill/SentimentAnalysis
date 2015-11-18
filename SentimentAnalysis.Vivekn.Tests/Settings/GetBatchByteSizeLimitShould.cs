namespace SentimentAnalysis.Vivekn.Tests.Settings
{
    using System;
    using System.Configuration;
    using AssertExLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetBatchByteSizeLimitShould : SettingsTest
    {
        [TestMethod]
        public void ReturnDefaultWhenNotSet()
        {
            ConfigurationManager.AppSettings[Constants.BatchByteSizeLimitConfigKey] = null;
            var result = Sut.GetBatchByteSizeLimit();
            Assert.AreEqual(Constants.DefaultBatchByteSizeLimit, result);
        }

        [TestMethod]
        public void ThrowInvalidCastExceptionWithCustomMessageWhenInvalidInConfiguration()
        {
            ConfigurationManager.AppSettings[Constants.BatchByteSizeLimitConfigKey] = "foo";

            AssertEx.Throws<InvalidCastException>(() => Sut.GetBatchByteSizeLimit());
        }

        [TestMethod]
        public void ReturnValueFromConfiguration()
        {
            const int expected = 500;
            ConfigurationManager.AppSettings[Constants.BatchByteSizeLimitConfigKey] = expected.ToString();

            var result = Sut.GetBatchByteSizeLimit();
            Assert.AreEqual(expected, result);
        }
    }
}