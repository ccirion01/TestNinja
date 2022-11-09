using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
   [TestFixture]
    public class BookingHelper_OverlappingBookingsExistTests
    {
        #region SetUp
        private Mock<IBookingRepository> _repo;
        private Booking _existingBooking;
        private int _newBookingId;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IBookingRepository>();
            _existingBooking = new Booking()
            {
                Id = 1,
                ArrivalDate = GetDate(2017, 01, 15),
                DepartureDate = GetDate(2017, 01, 20),
                Reference = "booking1"
            };
            _newBookingId = 2;
            _repo.Setup(r => r.GetActiveBookings(_newBookingId)).Returns(
                new List<Booking>() { _existingBooking }
                .AsQueryable()
            );
        } 
        #endregion

        #region No overlapping scenarios
        [Test]
        public void BookingsOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                _repo.Object,
                new Booking()
                {
                    Id = _newBookingId,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate),
                    Status = "Cancelled"
                });

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void BookingStartsAndFinishesBeforeExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                _repo.Object,
                new Booking()
                {
                    Id = _newBookingId,
                    ArrivalDate = Before(_existingBooking.ArrivalDate, 2),
                    DepartureDate = Before(_existingBooking.ArrivalDate)
                });

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void BookingStartsAndFinishesAfterExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                _repo.Object,
                new Booking()
                {
                    Id = _newBookingId,
                    ArrivalDate = After(_existingBooking.DepartureDate),
                    DepartureDate = After(_existingBooking.DepartureDate, days: 2)
                });

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        #endregion

        #region Overlapping scenarios
        [Test]
        public void SameArrivalDateButDifferentDepartureDate_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                _repo.Object,
                new Booking()
                {
                    Id = _newBookingId,
                    ArrivalDate = _existingBooking.ArrivalDate,
                    DepartureDate = After(_existingBooking.ArrivalDate)
                });

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void ArrivalDateContainedInExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                _repo.Object,
                new Booking()
                {
                    Id = _newBookingId,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.ArrivalDate)
                });

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void SameDepartureDateButDifferentArrivalDate_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                _repo.Object,
                new Booking()
                {
                    Id = _newBookingId,
                    ArrivalDate = After(_existingBooking.ArrivalDate, days: 2),
                    DepartureDate = _existingBooking.DepartureDate
                });

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void DepartureDateContainedInExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                _repo.Object,
                new Booking()
                {
                    Id = _newBookingId,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.ArrivalDate)
                });

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                _repo.Object,
                new Booking()
                {
                    Id = _newBookingId,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate)
                });

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                _repo.Object,
                new Booking()
                {
                    Id = _newBookingId,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = Before(_existingBooking.DepartureDate)
                });

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        #endregion

        #region Helpers
        private DateTime GetDate(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
        #endregion
    }
}
