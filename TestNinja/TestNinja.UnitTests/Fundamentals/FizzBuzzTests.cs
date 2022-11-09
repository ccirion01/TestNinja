using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void GetOutput_MultipleOfThree_ReturnFizz()
        {
            var result = FizzBuzz.GetOutput(6);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_MultipleOfFive_ReturnBuzz()
        {
            var result = FizzBuzz.GetOutput(10);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_MultipleOfThreeAndFive_ReturnFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_NotMultipleOfThreeOrFive_ReturnSameNumber()
        {
            var result = FizzBuzz.GetOutput(4);

            Assert.That(result, Is.EqualTo("4"));
        }
    }
}
