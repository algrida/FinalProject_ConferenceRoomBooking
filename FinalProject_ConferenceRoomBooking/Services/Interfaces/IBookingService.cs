using FinalProject_ConferenceRoomBooking.Data_Layer.Entities;

namespace FinalProject_ConferenceRoomBooking.Services.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<Bookings> GetAllBookings();
        Bookings GetBookingById(int id);
        IEnumerable<Bookings> GetBookingsByStatus(string status);
        Bookings GetBookingByDate (DateTime selectDate);
        void CreateBooking(Bookings newBooking);
        void UpdateBooking(Bookings updatedBooking);
        void DeleteBooking(int id);
        List<ReservationHolder> GetReservationHolders(string searchCriteria);
        ReservationHolder GetReservationHolderById(int id);
        void CreateReservationHolder(ReservationHolder reservationHolder);
        void UpdateReservationHolder(ReservationHolder updatedReservationHolder);
        void DeleteReservationHolder(int id);
        void SoftDeleteReservationHolder(int id);
    }
}