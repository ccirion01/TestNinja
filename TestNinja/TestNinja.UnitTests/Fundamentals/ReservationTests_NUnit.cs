 using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests_NUnit
    {
        //The convention is: MethodName_Scenario_ExpectedBehavior

        [Test]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {
            //Arrange
            var reservation = new Reservation();
            var user = new User() { IsAdmin = true };

            //Act
            var result = reservation.CanBeCancelledBy(user);

            //Assert
            Assert.IsTrue(result);
            Assert.That(result, Is.True);
            Assert.That(result == true);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            //Arrange
            var user = new User();
            var reservation = new Reservation() { MadeBy = user };

            //Act
            var result = reservation.CanBeCancelledBy(user);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            //Arrange
            var userThatMadeReservation = new User();
            var reservation = new Reservation() { MadeBy = userThatMadeReservation };
            var anotherUser = new User();

            //Act
            var result = reservation.CanBeCancelledBy(anotherUser);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
