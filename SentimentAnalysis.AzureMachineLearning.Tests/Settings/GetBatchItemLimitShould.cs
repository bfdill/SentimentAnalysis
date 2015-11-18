namespace SentimentAnalysis.AzureMachineLearning.Tests.Settings
{
    using System;
    using System.Configuration;
    using AssertExLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetBatchItemLimitShould : SettingsTest
    {
        [TestMethod]
        public void ReturnDefaultWhenNotSet()
        {
            ConfigurationManager.AppSettings[Constants.BatchItemLimitConfigKey] = null;
            var result = Sut.GetBatchItemLimit();
            Assert.AreEqual(Constants.DefaultBatchItemLimit, result);
        }

        [TestMethod]
        public void ThrowInvalidCastExceptionWithCustomMessageWhenInvalidInConfiguration()
        {
            ConfigurationManager.AppSettings[Constants.BatchItemLimitConfigKey] = "foo";

            AssertEx.Throws<InvalidCastException>(() => Sut.GetBatchItemLimit());
        }

        [TestMethod]
        public void ReturnValueFromConfiguration()
        {
            const int expected = 500;
            ConfigurationManager.AppSettings[Constants.BatchItemLimitConfigKey] = 500.ToString();

            var result = Sut.GetBatchItemLimit();
            Assert.AreEqual(expected, result);
        }
    }
}