using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_SpeedOutOfRange_ThrowArgumentOutOfRangeException(int speed)
        {
            Assert.That(
                () => _calculator.CalculateDemeritPoints(speed),
                Throws.Exception.TypeOf(typeof(ArgumentOutOfRangeException))
            );
        }

        //[Test]
        //public void CalculateDemeritPoints_SpeedLowerThanZero_ThrowArgumentOutOfRangeException()
        //{
        //    Assert.That(
        //        () => _calculator.CalculateDemeritPoints(-1), 
        //        Throws.Exception.TypeOf(typeof(ArgumentOutOfRangeException))
        //    );
        //}

        //[Test]
        //public void CalculateDemeritPoints_SpeedGreaterThanMaxSpeed_ThrowArgumentOutOfRangeException()
        //{
        //    Assert.That(
        //        () => _calculator.CalculateDemeritPoints(301),
        //        Throws.Exception.TypeOf(typeof(ArgumentOutOfRangeException))
        //    );
        //}

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(65, 0)]
        [TestCase(66, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int speed, int expectedResult)
        {
            var result = _calculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        //[Test]
        //public void CalculateDemeritPoints_SpeedIsZero_ReturnZero()
        //{
        //    var result = _calculator.CalculateDemeritPoints(0);

        //    Assert.That(result, Is.EqualTo(0));
        //}

        //[Test]
        //public void CalculateDemeritPoints_SpeedLowerThanLimit_ReturnZero()
        //{
        //    var result = _calculator.CalculateDemeritPoints(1);

        //    Assert.That(result, Is.EqualTo(0));
        //}

        //[Test]
        //public void CalculateDemeritPoints_SpeedEqualToLimit_ReturnZero()
        //{
        //    var result = _calculator.CalculateDemeritPoints(65);

        //    Assert.That(result, Is.EqualTo(0));
        //}

        //[Test]
        //public void CalculateDemeritPoints_SpeedGreaterThanLimit_ReturnDemeritPoints()
        //{
        //    var result = _calculator.CalculateDemeritPoints(70);

        //    Assert.That(result, Is.EqualTo(1));
        //}
    }
}