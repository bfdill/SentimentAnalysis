namespace SentimentAnalysis.AzureMachineLearning.Tests.Settings
{
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
    }
}