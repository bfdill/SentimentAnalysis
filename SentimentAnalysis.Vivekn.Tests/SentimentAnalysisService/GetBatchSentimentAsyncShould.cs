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
    using Constants = Vivekn.Constants;

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
        public async Task ReturnErrorWhenBatchSizeExceeded()
        {
            ConfigurationManager.AppSettings[Vivekn.Constants.BatchByteSizeLimitConfigKey] = "10";
            var sut = SentimentAnalysisTestHelper.BuildSut(SentimentAnalysisTestHelper.GetResponseMessageBatch());

            var expected = ViveknResult.Build(Constants.BatchByteSizeLimitExceededErrorText);
            var request = new Dictionary<string, string>() { { "A", "The quick brown fox jumps over the lazy dog." } };
            var result = await sut.GetBatchSentimentAsync(request);

            Assert.AreEqual(expected, result.First().Value);
            ConfigurationManager.AppSettings[Vivekn.Constants.BatchByteSizeLimitConfigKey] = null;
        }

        [TestMethod]
#pragma warning disable 1998
        public async Task ThrowIfInputIsNull()
#pragma warning restore 1998
        {
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
                @"[""this is the first request"",""this is the second request"",""this is the third request"",""this should cause an error""]";
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
                { "1", ViveknResult.Build(100M, Sentiment.Positive) },
                { "2", ViveknResult.Build(100M, Sentiment.Negative) }
            };

            var sut = SentimentAnalysisTestHelper.BuildSut(SentimentAnalysisTestHelper.GetResponseMessageBatch());

            var result = await sut.GetBatchSentimentAsync(_input);

            CollectionAssert.AreEquivalent(expected, result.ToList());
        }
    }
}