namespace SentimentAnalysis.AzureMachineLearning.Tests.Settings
{
    using System.Configuration;
    using System.Diagnostics;
    using DotNetTestHelper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AzureMachineLearningSettings = AzureMachineLearning.AzureMachineLearningSettings;

    [TestClass]
    public abstract class SettingsTest
    {
        protected AzureMachineLearningSettings Sut { get; set; }

        [TestInitialize]
        public void Init()
        {
            Sut = new SutBuilder<AzureMachineLearningSettings>().Build();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ConfigurationManager.AppSettings[Constants.PositiveSentimentThresholdConfigKey] = Constants.DefaultPositiveSentimentThreshold.ToString();
            ConfigurationManager.AppSettings[Constants.NegativeSentimentThresholdConfigKey] = Constants.DefaultNegativeSentimentThreshold.ToString();
            ConfigurationManager.AppSettings[Constants.ServiceBaseUriConfigKey] = Constants.DefaultServiceBaseUri;
            ConfigurationManager.AppSettings[Constants.BatchItemLimitConfigKey] = Constants.DefaultBatchItemLimit.ToString();
        }
    }
}