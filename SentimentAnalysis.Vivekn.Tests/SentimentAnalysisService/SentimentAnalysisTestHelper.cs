namespace SentimentAnalysis.Vivekn.Tests.SentimentAnalysisService
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Core;
    using Core.Domain;
    using DotNetTestHelper;
    using Moq;
    using SentimentAnalysisService = Vivekn.SentimentAnalysisService;

    internal class SentimentAnalysisTestHelper
    {
        public static SentimentAnalysisService BuildSut(HttpResponseMessage message)
        {
            var requestor = BuildMockRequestor(message);
            return BuildSut(requestor.Object);
        }

        public static SentimentAnalysisService BuildSut(ISentimentAnalysisRequestor requestor)
        {
            var sut = new SutBuilder<SentimentAnalysisService>()
                .AddDependency(requestor)
                .AddDependency(new ViveknSettings())
                .AddDependency(new ErrorMessageGenerator())
                .Build();

            return sut;
        }

        public static Mock<ISentimentAnalysisRequestor> BuildMockRequestor(HttpResponseMessage message)
        {
            var requestor = new Mock<ISentimentAnalysisRequestor>();
            requestor.Setup(r => r.PostAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(message);
            return requestor;
        }

        public static Mock<ISentimentAnalysisRequestor> BuildMockRequestorForPost(Action<string, string> callback, HttpResponseMessage message)
        {
            var requestor = new Mock<ISentimentAnalysisRequestor>();
            requestor.Setup(r => r.PostAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(message).Callback(callback);
            return requestor;
        }

        public static HttpResponseMessage GetErrorMessage(string error)
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(error) };
        }

        public static HttpResponseMessage GetResponseMessageSingle()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content =
                    new StringContent(
                        @"[{""confidence"": ""100.0000"", ""result"": ""Positive""}]")
            };
        }

        public static HttpResponseMessage GetResponseMessageBatch()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content =
                    new StringContent(
                        @"[{""confidence"": ""100.0000"", ""result"": ""Positive""}, {""confidence"": ""100.0000"", ""result"": ""Negative""}]")
            };
        }
    }
}