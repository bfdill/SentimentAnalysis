namespace SentimentAnalysis.Vivekn.Tests.SentimentAnalysisService
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AssertExLib;
    using Core;
    using Core.Domain;
    using Domain;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetBatchSentimentAsyncShould
    {
        private const string Error = "Mock Error String";

        private readonly Dictionary<string, string> _input = new Dictionary<string, string>
        {
            { "1", "This is the first request" },
            { "2", "This is the second request" },
            { "3", "This is the third request" },
            { "4", "this should cause an error" }
        };

        [TestMethod]
        public void ThrowIfOverBatchSizeLimit()
        {
            ConfigurationManager.AppSettings[Vivekn.Constants.BatchByteSizeLimitConfigKey] = "100";
            var sut = SentimentAnalysisTestHelper.BuildSut(SentimentAnalysisTestHelper.GetResponseMessageBatch());

            var requests = new Dictionary<string, string>();

            for (var i = 0; i < 11; i++)
            {
                requests.Add(i.ToString(), "this is text");
            }

            AssertEx.TaskThrows<InvalidOperationException>(() => sut.GetBatchSentimentAsync(requests));
        }

        [TestMethod]
        public async Task ThrowIfInputIsNull()
        {
            var expected = ViveknResult.Build(Core.Constants.SentimentNullInputErrorText);
            var sut = SentimentAnalysisTestHelper.BuildSut(SentimentAnalysisTestHelper.GetResponseMessageBatch());

            AssertEx.TaskThrows<ArgumentNullException>(async () => await sut.GetBatchSentimentAsync(null));
        }

        [TestMethod]
        public async Task ReturnAllFailureOnBadResult()
        {
            var err = new ErrorMessageGenerator().GenerateError(HttpStatusCode.BadRequest, Error);
            var expected = new Dictionary<string, Result>
            {
                { "1", ViveknResult.Build(err) },
                { "2", ViveknResult.Build(err) },
                { "3", ViveknResult.Build(err) },
                { "4", ViveknResult.Build(err) }
            };

            var sut = SentimentAnalysisTestHelper.BuildSut(SentimentAnalysisTestHelper.GetErrorMessage(Error));
            var result = await sut.GetBatchSentimentAsync(_input);
            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public async Task ReturnEmptyDictionaryForEmptyInput()
        {
            var expected = new Dictionary<string, Result>();
            var sut = SentimentAnalysisTestHelper.BuildSut(SentimentAnalysisTestHelper.GetResponseMessageBatch());

            var result = await sut.GetBatchSentimentAsync(new Dictionary<string, string>());
            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public async Task ConvertDictionaryToPostBody()
        {
            const string expected =
                @"{""Inputs"":[{""Id"":""1"",""Text"":""This is the first request""},{""Id"":""2"",""Text"":""This is the second request""},{""Id"":""3"",""Text"":""This is the third request""},{""Id"":""4"",""Text"":""this should cause an error""},]}";
            var request = string.Empty;

            var requestor = SentimentAnalysisTestHelper.BuildMockRequestorForPost((req, body) => request = body, SentimentAnalysisTestHelper.GetResponseMessageBatch());
            var sut = SentimentAnalysisTestHelper.BuildSut(requestor.Object);
            await sut.GetBatchSentimentAsync(_input);

            Assert.AreEqual(expected, request);
        }

        [TestMethod]
        public async Task DecodeResponse()
        {
            var expected = new Dictionary<string, Result>
            {
                { "1", ViveknResult.Build(0.9549767M, Sentiment.Positive) },
                { "2", ViveknResult.Build(0.7767222M, Sentiment.Positive) },
                { "3", ViveknResult.Build(0.8988889M, Sentiment.Positive) },
                { "4", ViveknResult.Build("Record cannot be null/empty") }
            };

            var sut = SentimentAnalysisTestHelper.BuildSut(SentimentAnalysisTestHelper.GetResponseMessageBatch());

            var result = await sut.GetBatchSentimentAsync(_input);

            CollectionAssert.AreEquivalent(expected, result.ToList());
        }
    }
}