namespace SentimentAnalysis.Vivekn.Tests.Settings
{
    using System.Configuration;
    using DotNetTestHelper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class SettingsTest
    {
        protected ViveknSettings Sut { get; set; }

        [TestInitialize]
        public void Init()
        {
            Sut = new SutBuilder<ViveknSettings>().Build();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ConfigurationManager.AppSettings[Constants.ServiceBaseUriConfigKey] = null;
            ConfigurationManager.AppSettings[Constants.BatchByteSizeLimitConfigKey] = Constants.DefaultBatchByteSizeLimit.ToString();
        }
    }
}