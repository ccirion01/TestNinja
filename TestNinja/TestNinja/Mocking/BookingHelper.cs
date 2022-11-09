using System;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        /* Option 1: extract UnitOfWork into an interface and inject to the method.
         * Option 2: use repository pattern to encapsulate the logic of getting the cancelled bookings.
         */

        public static string OverlappingBookingsExist(IBookingRepository repository, Booking booking)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = repository.GetActiveBookings(booking.Id);

            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate < b.DepartureDate
                        && b.ArrivalDate < booking.DepartureDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}