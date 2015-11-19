using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.Vivekn.Tests.SentimentAnalysisService
{
    using System.Net.Http;
    using AssertExLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetSentimentAsyncShould
    {
        [TestMethod]
        public async Task ThrowIfInputIsNull()
        {
            var sut = SentimentAnalysisTestHelper.BuildSut(SentimentAnalysisTestHelper.GetResponseMessageSingle());

            AssertEx.TaskThrows<ArgumentNullException>(async () => await sut.GetSentimentAsync(null));
        }
    }
}
