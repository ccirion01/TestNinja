using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            var formatter = new HtmlFormatter();
            string str = "abc";

            var result = formatter.FormatAsBold(str);

            //Specific -> in some cases, specificity is needed
            Assert.That(result, Is.EqualTo($"<strong>{str}</strong>").IgnoreCase);

            //More general -> in some other cases, a more general check is preferred
            Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
            Assert.That(result, Does.EndWith("</strong>").IgnoreCase);
            Assert.That(result, Does.Contain(str));
        }
    }
}
