using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _storage;
        private EmployeeController _controller;

        [SetUp]
        public void SetUp()
        {
            _storage = new Mock<IEmployeeStorage>();
            _controller = new EmployeeController(_storage.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromStorage()
        {
            _controller.DeleteEmployee(1);

            //We verify that the Delete method from IEmployeeStorage was called passing in the same id that we passed to the Controller.
            _storage.Verify(s => s.Delete(1));
        }

        [Test]
        public void DeleteEmployee_WhenCalled_CallRedirectToAction()
        {
            var result = _controller.DeleteEmployee(1);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }
    }
}
