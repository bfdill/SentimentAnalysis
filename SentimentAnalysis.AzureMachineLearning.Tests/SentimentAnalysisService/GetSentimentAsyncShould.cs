namespace SentimentAnalysis.AzureMachineLearning.Tests.SentimentAnalysisService
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AssertExLib;
    using Core;
    using Domain;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetSentimentAsyncShould
    {
        [TestMethod]
#pragma warning disable 1998
        public async Task ThrowIfInputIsNull()
#pragma warning restore 1998
        {
            var sut = SentimentAnalysisTestHelper.BuildSut(GetMessage());

            AssertEx.TaskThrows<ArgumentNullException>(async () => await sut.GetSentimentAsync(null));
        }

        private static HttpResponseMessage GetMessage()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content =
                    new StringContent(
                        @"{""odata.metadata"":""https://api.datamarket.azure.com/data.ashx/amla/text-analytics/v1/$metadata"",""Score"":1.0}")
            };
        }
    }
}