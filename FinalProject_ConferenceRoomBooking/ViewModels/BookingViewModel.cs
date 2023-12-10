using FinalProject_ConferenceRoomBooking.Data_Layer.Entities;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_ConferenceRoomBooking.ViewModels
{
    public class BookingViewModel
    {
        public string Code { get; internal set; }
        public bool IsConfirmed { get; set; }
        public bool isDeleted { get; set; }
        public string ConferenceRoomCode { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int RoomId { get; set; }
        public int NumberOfPeople { get; set; }
        public string Status { get; internal set; }
        public ReservationHolder ReservationHolder { get; set; }
    }
}
