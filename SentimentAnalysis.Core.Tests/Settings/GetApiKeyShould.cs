namespace SentimentAnalysis.Core.Tests.Settings
{
    using System;
    using System.Configuration;
    using AssertExLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetApiKeyShould : SettingsTest
    {
        [TestMethod]
        public void ThrowIfApiKeyNotSetInConfiguration()
        {
            AssertEx.Throws<InvalidOperationException>(() => Sut.GetApiKey());
        }

        [TestMethod]
        public void PullKeyFromConfiguration()
        {
            const string expected = "my account key is set here";
            ConfigurationManager.AppSettings[Constants.AccountConfigKey] = expected;

            Assert.AreEqual(expected, Sut.GetApiKey());
        }
    }
}