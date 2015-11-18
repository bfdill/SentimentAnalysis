namespace SentimentAnalysis.Integration.Tests.Vivekn
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SentimentAnalysis.Vivekn;
    using SentimentAnalysis.Vivekn.Domain;

    [TestClass]
    public class GetSentimentShould
    {
        [TestMethod]
        public async Task ReturnSinglePositiveWhenExpected()
        {
            var settings = new ViveknSettings();
            var sut = new SentimentAnalysisService(new SentimentAnalysisRequestor(settings), new ErrorMessageGenerator(), settings);

            var source = ReferenceData.PostiviteResultOne;
            var expected = ViveknResult.Build(source.Confidence, source.Sentiment);
            var actual = await sut.GetSentimentAsync(source.Text);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task ReturnSingleNegativeWhenExpected()
        {
            var settings = new ViveknSettings();
            var sut = new SentimentAnalysisService(new SentimentAnalysisRequestor(settings), new ErrorMessageGenerator(), settings);

            var source = ReferenceData.NegativeResultOne;
            var expected = ViveknResult.Build(source.Confidence, source.Sentiment);
            var actual = await sut.GetSentimentAsync(source.Text);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task ReturnExpectedBatch()
        {
            var settings = new ViveknSettings();
            var sut = new SentimentAnalysisService(new SentimentAnalysisRequestor(settings), new ErrorMessageGenerator(), settings);
            var results = await sut.GetBatchSentimentAsync(new Dictionary<string, string> { { ReferenceData.PostiviteResultOne.Id, ReferenceData.PostiviteResultOne.Text }, { ReferenceData.NegativeResultOne.Id, ReferenceData.NegativeResultOne.Text } });

            var expected = ViveknResult.Build(ReferenceData.PostiviteResultOne.Confidence, ReferenceData.PostiviteResultOne.Sentiment);
            var actual = results.First().Value;

            Assert.AreEqual(expected, actual);

            expected = ViveknResult.Build(ReferenceData.NegativeResultOne.Confidence, ReferenceData.NegativeResultOne.Sentiment);
            actual = results.Last().Value;

            Assert.AreEqual(expected, actual);
        }
    }
}