﻿namespace SentimentAnalysis.Core.Tests.RequestHeaderFactory
{
    using System;
    using System.Text;
    using Domain;
    using DotNetTestHelper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using RequestHeaderFactory = Core.RequestHeaderFactory;

    [TestClass]
    public class AuthorizationHeaderShould
    {
        private const string TestBasicCredentials = "TestUserName:TestPassword";

        [TestMethod]
        public void CreateHeader()
        {
            var expected = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(TestBasicCredentials))}";
            var sut = new SutBuilder<RequestHeaderFactory>().AddDependency(CreateSettings()).Build();

            Assert.AreEqual(expected, sut.AuthorizationHeader());
        }

        private static ISettings CreateSettings()
        {
            var settings = new Mock<ISettings>();
            settings.Setup(s => s.GetBasicAuthenticationCredentials()).Returns(TestBasicCredentials);
            return settings.Object;
        }
    }
}