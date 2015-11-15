namespace SentimentAnalysis.Core.Tests.Settings
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Settings = Core.Settings;

    [TestClass]
    public abstract class SettingsTest
    {
        protected Settings Sut { get; set; }

        [TestInitialize]
        public void Init()
        {
            //Sut = new SutBuilder<Settings>().Build();
            Sut = new Settings();
        }
    }
}