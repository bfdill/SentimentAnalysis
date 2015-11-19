namespace SentimentAnalysis.AzureMachineLearning.Tests.SentimentAnalysisService
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AssertExLib;
    using Core;
    using Core.Domain;
    using Domain;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Settings;
    using Constants = AzureMachineLearning.Constants;

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
        public void ThrowIfOverBatchItemLimit()
        {
            ConfigurationManager.AppSettings[Constants.BatchItemLimitConfigKey] = "10";
            var sut = SentimentAnalysisTestHelper.BuildSut(GetMessage());

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
            var expected = AzureMachineLearningResult.Build(Core.Constants.SentimentNullInputErrorText);
            var sut = SentimentAnalysisTestHelper.BuildSut(GetMessage());

            AssertEx.TaskThrows<ArgumentNullException>(async () => await sut.GetBatchSentimentAsync(null));
        }

        [TestMethod]
        public async Task ReturnAllFailureOnBadResult()
        {
            var err = new ErrorMessageGenerator().GenerateError(HttpStatusCode.BadRequest, Error);
            var expected = new Dictionary<string, Result>
            {
                { "1", AzureMachineLearningResult.Build(err) },
                { "2", AzureMachineLearningResult.Build(err) },
                { "3", AzureMachineLearningResult.Build(err) },
                { "4", AzureMachineLearningResult.Build(err) }
            };

            var sut = SentimentAnalysisTestHelper.BuildSut(SentimentAnalysisTestHelper.GetErrorMessage(Error));
            var result = await sut.GetBatchSentimentAsync(_input);
            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public async Task ReturnEmptyDictionaryForEmptyInput()
        {
            var expected = new Dictionary<string, Result>();
            var sut = SentimentAnalysisTestHelper.BuildSut(GetMessage());

            var result = await sut.GetBatchSentimentAsync(new Dictionary<string, string>());
            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public async Task ConvertDictionaryToPostBody()
        {
            const string expected =
                @"{""Inputs"":[{""Id"":""1"",""Text"":""This is the first request""},{""Id"":""2"",""Text"":""This is the second request""},{""Id"":""3"",""Text"":""This is the third request""},{""Id"":""4"",""Text"":""this should cause an error""},]}";
            var request = string.Empty;

            var requestor = SentimentAnalysisTestHelper.BuildMockRequestorForPost((req, body) => request = body, GetMessage());
            var sut = SentimentAnalysisTestHelper.BuildSut(requestor.Object);
            await sut.GetBatchSentimentAsync(_input);

            Assert.AreEqual(expected, request);
        }

        [TestMethod]
        public async Task DecodeResponse()
        {
            var expected = new Dictionary<string, Result>
            {
                { "1", AzureMachineLearningResult.Build(0.9549767M, Sentiment.Positive) },
                { "2", AzureMachineLearningResult.Build(0.7767222M, Sentiment.Positive) },
                { "3", AzureMachineLearningResult.Build(0.8988889M, Sentiment.Positive) },
                { "4", AzureMachineLearningResult.Build("Record cannot be null/empty") }
            };
            var sut = SentimentAnalysisTestHelper.BuildSut(GetMessage());

            var result = await sut.GetBatchSentimentAsync(_input);
            CollectionAssert.AreEquivalent(expected, result.ToList());
        }

        private static HttpResponseMessage GetMessage()
        {
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(@"{
  ""odata.metadata"":""https://api.datamarket.azure.com/data.ashx/amla/text-analytics/v1/$metadata"", ""SentimentBatch"":
    [{""Score"":0.9549767,""Id"":""1""},
     {""Score"":0.7767222,""Id"":""2""},
     {""Score"":0.8988889,""Id"":""3""}
    ],  
    ""Errors"":[
       {""Id"": ""4"", Message:""Record cannot be null/empty""}
    ]
}") };
        }
    }
}