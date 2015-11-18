namespace SentimentAnalysis.AzureMachineLearning.Tests.Settings
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using AssertExLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetPositiveSentimentThresholdShould : SettingsTest
    {
        [TestMethod]
        public void ReturnDefaultWhenNotSet()
        {
            ConfigurationManager.AppSettings[Constants.PositiveSentimentThresholdConfigKey] = null;
            var result = Sut.GetPositiveSentimentThreshold();
            Assert.AreEqual(Constants.DefaultPositiveSentimentThreshold, result);
        }

        [TestMethod]
        public void ThrowInvalidCastExceptionWithCustomMessageWhenInvalidInConfiguration()
        {
            ConfigurationManager.AppSettings[Constants.PositiveSentimentThresholdConfigKey] = "foo";

            AssertEx.Throws<InvalidCastException>(() => Sut.GetPositiveSentimentThreshold());
        }

        [TestMethod]
        public void ReturnValueFromConfiguration()
        {
            const decimal expected = 500m;
            ConfigurationManager.AppSettings[Constants.PositiveSentimentThresholdConfigKey] = expected.ToString(CultureInfo.CurrentCulture);

            var result = Sut.GetPositiveSentimentThreshold();
            Assert.AreEqual(expected, result);
        }
    }
}