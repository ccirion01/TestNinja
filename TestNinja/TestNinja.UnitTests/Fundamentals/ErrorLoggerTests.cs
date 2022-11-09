using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        private ErrorLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }

        [Test]
        public void Log_ValidError_SetLastErrorProperty()
        {            
            string errorMsg = "some error";

            _logger.Log(errorMsg);
             
            Assert.That(_logger.LastError, Is.EqualTo(errorMsg));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string errorMsg)
        {
            Assert.That(() => _logger.Log(errorMsg), Throws.ArgumentNullException);
        }

        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var eventId = Guid.Empty;
            _logger.ErrorLogged += (sender, args) => { eventId = args; };

            _logger.Log("some error");

            Assert.That(eventId, Is.Not.EqualTo(Guid.Empty));
        }
    }
}