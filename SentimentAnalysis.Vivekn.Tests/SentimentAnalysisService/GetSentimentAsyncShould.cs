namespace SentimentAnalysis.Vivekn.Tests.SentimentAnalysisService
{
    using System;
    using System.Threading.Tasks;
    using AssertExLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetSentimentAsyncShould
    {
        [TestMethod]
#pragma warning disable 1998
        public async Task ThrowIfInputIsNull()
#pragma warning restore 1998
        {
            var sut = SentimentAnalysisTestHelper.BuildSut(SentimentAnalysisTestHelper.GetResponseMessageSingle());

            AssertEx.TaskThrows<ArgumentNullException>(async () => await sut.GetSentimentAsync(null));
        }
    }
}