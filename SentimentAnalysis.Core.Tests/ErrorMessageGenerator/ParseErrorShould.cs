namespace SentimentAnalysis.Core.Tests.ErrorMessageGenerator
{
    using System.Net;
    using DotNetTestHelper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ErrorMessageGenerator = Core.ErrorMessageGenerator;

    [TestClass]
    public class ParseErrorShould
    {
        [TestMethod]
        public void GenerateFormattedErrorMessage()
        {
            const string expected = "400 BadRequest: This isn't going to happen";
            var sut = new SutBuilder<ErrorMessageGenerator>().Build();
            var result = sut.GenerateError(HttpStatusCode.BadRequest, "This isn't going to happen");
            Assert.AreEqual(expected, result);
        }
    }
}