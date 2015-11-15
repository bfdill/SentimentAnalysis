namespace SentimentAnalysis.Core.Tests.Settings
{
    using DotNetTestHelper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Settings = Core.Settings;

    [TestClass]
    public abstract class SettingsTest
    {
        protected Settings Sut;

        [TestInitialize]
        public void Init()
        {
            //Sut = new SutBuilder<Settings>().Build();
            Sut = new Settings();
        }
    }
}