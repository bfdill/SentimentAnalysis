namespace SentimentAnalysis.AzureMachineLearning.Tests.Settings
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using AssertExLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetNegativeSentimentThresholdShould : SettingsTest
    {
        [TestMethod]
        public void ReturnDefaultWhenNotSet()
        {
            ConfigurationManager.AppSettings[Constants.NegativeSentimentThresholdConfigKey] = null;
            var result = Sut.GetNegativeSentimentThreshold();
            Assert.AreEqual(Constants.DefaultNegativeSentimentThreshold, result);
        }

        [TestMethod]
        public void ThrowInvalidCastExceptionWithCustomMessageWhenInvalidInConfiguration()
        {
            ConfigurationManager.AppSettings[Constants.NegativeSentimentThresholdConfigKey] = "foo";

            AssertEx.Throws<InvalidCastException>(() => Sut.GetNegativeSentimentThreshold());
        }

        [TestMethod]
        public void ReturnValueFromConfiguration()
        {
            const decimal expected = 500m;
            ConfigurationManager.AppSettings[Constants.NegativeSentimentThresholdConfigKey] = expected.ToString(CultureInfo.CurrentCulture);

            var result = Sut.GetNegativeSentimentThreshold();
            Assert.AreEqual(expected, result);
        }
    }
}