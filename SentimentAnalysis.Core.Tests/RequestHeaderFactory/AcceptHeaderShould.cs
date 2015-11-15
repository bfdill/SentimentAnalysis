﻿namespace SentimentAnalysis.Core.Tests.RequestHeaderFactory
{
    using System.Net.Http.Headers;
    using DotNetTestHelper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RequestHeaderFactory = Core.RequestHeaderFactory;

    [TestClass]
    public class AcceptHeaderShould
    {
        [TestMethod]
        public void CreateAcceptHeader()
        {
            var expected = new MediaTypeWithQualityHeaderValue("application/json");
            var sut = new SutBuilder<RequestHeaderFactory>().Build();
            Assert.AreEqual(expected, sut.AcceptHeader());
        }
    }
}