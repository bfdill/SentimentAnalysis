namespace SentimentAnalysis.Core.Tests.Settings
{
    using System;
    using System.Configuration;
    using AssertExLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetBasicAuthenticationCredentialsShould : SettingsTest
    {
        [TestMethod]
        public void ThrowIfApiKeyNotSetInConfiguration()
        {
            AssertEx.Throws<InvalidOperationException>(() => Sut.GetBasicAuthenticationCredentials());
        }

        [TestMethod]
        public void PullKeyFromConfiguration()
        {
            const string expected = "Username:Password";
            ConfigurationManager.AppSettings[Constants.BasicAuthenticationCredentialsConfigKey] = expected;

            Assert.AreEqual(expected, Sut.GetBasicAuthenticationCredentials());
        }
    }
}