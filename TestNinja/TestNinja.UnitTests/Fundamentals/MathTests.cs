using NUnit.Framework;
using System.Linq;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        //SetUp
        //TearDown

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        //[Ignore("Because I want to")]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            //Act
            var result = _math.Add(1, 2);

            //Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        //[Test]
        //public void Max_FirstArgumentIsGreater_ReturnTheFirstArgument()
        //{
        //    var result = _math.Max(2, 1);

        //    Assert.That(result, Is.EqualTo(2));
        //}

        //[Test]
        //public void Max_SecondArgumentIsGreater_ReturnTheSecondArgument()
        //{
        //    var result = _math.Max(1, 2);

        //    Assert.That(result, Is.EqualTo(2));
        //}

        //[Test]
        //public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
        //{
        //    var result = _math.Max(1, 1);

        //    Assert.That(result, Is.EqualTo(1));
        //}

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            //Too general
            //Assert.That(result, Is.Not.Empty);

            //Assert.That(result.Count(), Is.EqualTo(3));

            //More specific
            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            //A simplest way to do this
            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            //More specific checks
            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);
        }

        [Test]
        public void GetOddNumbers_LimitIsLowerThanZero_ReturnEmptyArray()
        {
            var result = _math.GetOddNumbers(-1);

            //A simplest way to do this
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetOddNumbers_LimitIsZero_ReturnEmptyArray()
        {
            var result = _math.GetOddNumbers(0);

            //A simplest way to do this
            Assert.That(result, Is.Empty);
        }
    }
}
