namespace SentimentAnalysis.Vivekn.Tests.Settings
{
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
    }
}