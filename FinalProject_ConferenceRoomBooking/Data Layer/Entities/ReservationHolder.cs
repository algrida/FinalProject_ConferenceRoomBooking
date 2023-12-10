using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject_ConferenceRoomBooking.Data_Layer.Entities
{
    public class ReservationHolder
    {
        public int Id { get; set; }
        public string IdCardNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Notes { get; set; }

        // Navigation property to link to Booking
        public int BookingId { get; set; }
        public Bookings Bookings { get; set; }
        public bool IsDeleted { get; internal set; }
    }
}
