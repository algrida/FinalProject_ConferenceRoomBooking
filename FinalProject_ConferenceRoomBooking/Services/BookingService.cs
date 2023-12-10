using FinalProject_ConferenceRoomBooking.Data;
using FinalProject_ConferenceRoomBooking.Data_Layer.Entities;
using FinalProject_ConferenceRoomBooking.Services;
using FinalProject_ConferenceRoomBooking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_ConferenceRoomBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateBooking(Bookings newBooking)
        {
            ValidateBooking(newBooking);

            // Perform any additional logic before creating the booking

            newBooking.GenerateBookingCode("C001");  // Generate booking code

            _context.Bookings.Add(newBooking);
            _context.SaveChanges();
        }

        public void DeleteBooking(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bookings> GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public Bookings GetBookingByDate(DateTime selectDate)
        {
            throw new NotImplementedException();
        }

        public Bookings GetBookingById(int id)
        {
            throw new NotImplementedException();
        }

        public Bookings GetBookingByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bookings> GetBookingsByStatus(string status)
        {
            throw new NotImplementedException();
            _context.Bookings.Where(b => b.Status == status).ToList();
        }

        public void UpdateBooking(Bookings updatedBooking)
        {
            throw new NotImplementedException();
        }

        private void ValidateBooking(Bookings newBooking)
        {
            // Check if the number of people attending exceeds the maximum capacity
            var room = _context.ConferenceRooms.Find(newBooking.RoomId);
            if (room != null && newBooking.NumberOfPeople > room.MaximumCapacity)
            {
                throw new InvalidOperationException("Number of people exceeds maximum capacity.");
            }

            // Check if the start date is in the past
            if (newBooking.StartDate < DateTime.Now)
            {
                throw new InvalidOperationException("Booking start date cannot be in the past.");
            }

            // Check if the end date is before the start date
            if (newBooking.EndDate <= newBooking.StartDate)
            {
                throw new InvalidOperationException("Booking end date must be after the start date.");
            }
        }
        public List<ReservationHolder> GetReservationHolders(string searchCriteria)
        {
            return _context.ReservationHolders
                .Where(rh => rh.IdCardNumber.Contains(searchCriteria) ||
                             rh.Name.Contains(searchCriteria) ||
                             rh.Surname.Contains(searchCriteria) ||
                             rh.Email.Contains(searchCriteria) ||
                             rh.PhoneNumber.Contains(searchCriteria))
                .ToList();
        }

        public ReservationHolder GetReservationHolderById(int id)
        {
            return _context.ReservationHolders.Find(id);
        }

        public void CreateReservationHolder(ReservationHolder reservationHolder)
        {
            _context.ReservationHolders.Add(reservationHolder);
            _context.SaveChanges();
        }

        public void UpdateReservationHolder(ReservationHolder updatedReservationHolder)
        {
            _context.Entry(updatedReservationHolder).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteReservationHolder(int id)
        {
            ReservationHolder reservationHolder = _context.ReservationHolders.Find(id);
            if (reservationHolder != null)
            {
                _context.ReservationHolders.Remove(reservationHolder);
                _context.SaveChanges();
            }
        }

        public void SoftDeleteReservationHolder(int id)
        {
            if (!IsUserAdmin())
            {
                throw new UnauthorizedAccessException("Only admins are allowed to delete account holders.");
            }

            ReservationHolder reservationHolder = _context.ReservationHolders.Find(id);

            if (reservationHolder != null)
            {
                reservationHolder.IsDeleted = true;
                _context.SaveChanges();

                DeleteBookingsByReservationHolderId(id);
            }
            else
            {
                throw new InvalidOperationException($"Account holder with ID {id} not found.");
            }
        }

        private void DeleteBookingsByReservationHolderId(int reservationHolderId)
        {
            List<Bookings> associatedBookings = _context.Bookings
                .Where(b => b.ReservationHolderId == reservationHolderId)
                .ToList();

            foreach (var booking in associatedBookings)
            {
                booking.IsDeleted = true;
            }

            _context.SaveChanges();
        }
        private bool IsUserAdmin()
        {
            // Implement your admin check logic here
            // Example: You might use roles, claims, or any other authentication mechanism
            // For simplicity, you might want to implement a proper authentication and authorization mechanism
            return User.IsInRole("Admin");
        }
    }
}