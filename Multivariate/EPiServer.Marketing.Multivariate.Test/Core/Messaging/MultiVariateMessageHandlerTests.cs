﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPiServer.ServiceLocation;
using Moq;
using EPiServer.Marketing.Multivariate.Messaging;
using EPiServer.Marketing.Multivariate.Dal;
using EPiServer.Marketing.Multivariate.Model;
using System.Collections.Generic;

namespace EPiServer.Marketing.Multivariate.Test.Messaging
{
    [TestClass]
    public class MultiVariateMessageHandlerTests
    {
        private Mock<IServiceLocator> _serviceLocator;
        private Mock<IMultivariateTestManager> _testManager;

        static Guid testGuid = Guid.NewGuid();
        static Guid original = Guid.NewGuid();
        static Guid varient = Guid.NewGuid();

        MultivariateTest test = new MultivariateTest()
        {
            Id = testGuid,
            Title = "SomeTest",
            MultivariateTestResults = new List<MultivariateTestResult>() {
                new MultivariateTestResult() { ItemId = original, Conversions = 5, Views = 10 },
                new MultivariateTestResult() { ItemId = varient, Conversions = 100, Views = 200 },
                new MultivariateTestResult() { }
            }
        };

        private MultiVariateMessageHandler GetUnitUnderTest()
        {
            _serviceLocator = new Mock<IServiceLocator>();
            _testManager = new Mock<IMultivariateTestManager>();
            _serviceLocator.Setup(sl => sl.GetInstance<IMultivariateTestManager>()).Returns(_testManager.Object);

            return new MultiVariateMessageHandler(_serviceLocator.Object);
        }

        [TestMethod]
        public void UpdateConversionUpdatesCorrectValue()
        {
            var messageHandler = GetUnitUnderTest();

            messageHandler.Handle( new UpdateConversionsMessage() { TestId = testGuid, VariantId = varient } );

            // Verify that save is called and conversion value is correct
            _testManager.Verify(tm => tm.IncrementCount(It.Is<Guid>( gg =>  gg.Equals(testGuid)),
                It.Is<Guid>(gg => gg.Equals(varient)), It.Is<Model.Enums.CountType>(ct => ct.Equals(Model.Enums.CountType.Conversion))) ,
                Times.Once, "Repository save was not called or conversion value is not as expected") ;
        }

        [TestMethod]
        public void UpdateViewUpdatesCorrectValue()
        {
            var messageHandler = GetUnitUnderTest();

            messageHandler.Handle(new UpdateViewsMessage() { TestId = testGuid, VariantId = varient });
            // Verify that save is called and conversion value is correct

            _testManager.Verify(tm => tm.IncrementCount(It.Is<Guid>(gg => gg.Equals(testGuid)),
                It.Is<Guid>(gg => gg.Equals(varient)), It.Is<Model.Enums.CountType>(ct => ct.Equals(Model.Enums.CountType.View))),
                Times.Once, "Repository save was not called or view value is not as expected");

        }
    }
}
